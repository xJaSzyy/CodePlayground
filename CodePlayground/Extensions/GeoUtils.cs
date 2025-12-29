using CodePlayground.Services;

namespace CodePlayground.Extensions;

public static class GeoUtils
{
    private const double EarthRadiusMeters = 6371000;

    public static double DistanceMeters(Coordinates a, Coordinates b)
    {
        var lat1 = DegreesToRadians(a.Lat);
        var lon1 = DegreesToRadians(a.Lon);
        var lat2 = DegreesToRadians(b.Lat);
        var lon2 = DegreesToRadians(b.Lon);

        var dLat = lat2 - lat1;
        var dLon = lon2 - lon1;

        var sinLat = Math.Sin(dLat / 2);
        var sinLon = Math.Sin(dLon / 2);

        var h = sinLat * sinLat +
                Math.Cos(lat1) * Math.Cos(lat2) *
                sinLon * sinLon;

        var c = 2 * Math.Atan2(Math.Sqrt(h), Math.Sqrt(1 - h));

        return EarthRadiusMeters * c;
    }

    private static double DegreesToRadians(double deg) => deg * Math.PI / 180.0;

    public static List<Coordinates> MergeAll(List<List<Coordinates>> lists, double maxGapMeters = 0)
    {
        if (lists.Count == 0)
            return new List<Coordinates>();

        var chain = new List<Coordinates>(lists[0]);
        lists.RemoveAt(0);

        while (lists.Count > 0)
        {
            var bestIndex = -1;
            var bestReverse = false;
            var bestAttachToStart = false;
            var bestDist = double.MaxValue;

            var chainStart = chain[0];
            var chainEnd = chain[^1];

            for (int i = 0; i < lists.Count; i++)
            {
                var seg = lists[i];
                var segStart = seg[0];
                var segEnd = seg[^1];

                // цепочка.начало ↔ сегмент.начало
                double d = GeoUtils.DistanceMeters(chainStart, segStart);
                if (d < bestDist)
                {
                    bestDist = d;
                    bestIndex = i;
                    bestReverse = true; // надо развернуть сегмент (чтоб конец к началу цепочки)
                    bestAttachToStart = true;
                }

                // цепочка.начало ↔ сегмент.конец
                d = GeoUtils.DistanceMeters(chainStart, segEnd);
                if (d < bestDist)
                {
                    bestDist = d;
                    bestIndex = i;
                    bestReverse = false; // уже конец сегмента
                    bestAttachToStart = true;
                }

                // цепочка.конец ↔ сегмент.начало
                d = GeoUtils.DistanceMeters(chainEnd, segStart);
                if (d < bestDist)
                {
                    bestDist = d;
                    bestIndex = i;
                    bestReverse = false;
                    bestAttachToStart = false; // к концу цепочки
                }

                // цепочка.конец ↔ сегмент.конец
                d = GeoUtils.DistanceMeters(chainEnd, segEnd);
                if (d < bestDist)
                {
                    bestDist = d;
                    bestIndex = i;
                    bestReverse = true;
                    bestAttachToStart = false;
                }
            }

            if (bestIndex == -1 || bestDist > maxGapMeters)
                break;

            var best = lists[bestIndex];
            lists.RemoveAt(bestIndex);

            if (bestReverse)
                best.Reverse();

            if (bestAttachToStart)
            {
                if (DistanceMeters(best[^1], chainStart) < 1e-3)
                {
                    best.RemoveAt(best.Count - 1);
                }

                chain.InsertRange(0, best);
            }
            else
            {
                if (DistanceMeters(chainEnd, best[0]) < 1e-3)
                {
                    best.RemoveAt(0);
                }

                chain.AddRange(best);
            }
        }

        return chain;
    }
}