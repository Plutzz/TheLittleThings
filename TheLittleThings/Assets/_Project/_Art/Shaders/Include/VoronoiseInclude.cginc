float3 Hash3D(float3 p) {
    p = frac(p * 0.8);
    p += dot(p, p + 19.19);
    return frac(float3(p.x * p.y, p.y * p.z, p.z * p.x));
}

float VoronoiDistanceToEdge3D(float3 Coord, float Scale, float Randomness) {
    float3 CoordScaled = Coord * Scale;
    float3 CellPosition = floor(CoordScaled);
    float3 LocalPosition = CoordScaled - CellPosition;

    float3 VectorToClosest = float3(0, 0, 0);
    float minDist = 1e8;
    for (int k = -1; k <= 1; k++) {
        for (int j = -1; j <= 1; j++) {
            for (int i = -1; i <= 1; i++) {
                float3 Offset = float3(i, j, k);
                float3 VectorToPoint = Offset + Hash3D(CellPosition + Offset) * Randomness - LocalPosition;
                float Dist = dot(VectorToPoint, VectorToPoint);
                if (Dist < minDist) {
                    minDist = Dist;
                    VectorToClosest = VectorToPoint;
                }
            }
        }
    }

    minDist = 1e8;
    for (int k = -1; k <= 1; k++) {
        for (int j = -1; j <= 1; j++) {
            for (int i = -1; i <= 1; i++) {
                float3 Offset = float3(i, j, k);
                float3 VectorToPoint = Offset + Hash3D(CellPosition + Offset) * Randomness - LocalPosition;
                float3 PerpendicularToEdge = VectorToPoint - VectorToClosest;
                if (dot(PerpendicularToEdge, PerpendicularToEdge) > 0.0001) {
                    float Dist = dot((VectorToClosest + VectorToPoint) / 2.0, normalize(PerpendicularToEdge));
                    minDist = min(minDist, Dist);
                }  
            }
        }
    }

    return minDist;
}

float VoronoiDistanceToCenter3D(float3 Coord, float Scale, float Randomness) {
    float3 CoordScaled = Coord * Scale;
    float3 CellPosition = floor(CoordScaled);
    float3 LocalPosition = CoordScaled - CellPosition;

    float3 VectorToClosest = float3(0, 0, 0);
    float minDist = 1e8;
    for (int k = -1; k <= 1; k++) {
        for (int j = -1; j <= 1; j++) {
            for (int i = -1; i <= 1; i++) {
                float3 Offset = float3(i, j, k);
                float3 VectorToPoint = Offset + Hash3D(CellPosition + Offset) * Randomness - LocalPosition;
                float Dist = dot(VectorToPoint, VectorToPoint);
                if (Dist < minDist) {
                    minDist = Dist;
                    VectorToClosest = VectorToPoint;
                }
            }
        }
    }

    return dot(VectorToClosest, VectorToClosest);
}

void Voronoi3D_float(float3 Coord, float Scale, float Randomness, out float DistanceFromEdge, out float DistanceFromCenter) {
    DistanceFromEdge = VoronoiDistanceToEdge3D(Coord, Scale, Randomness);
    DistanceFromCenter = VoronoiDistanceToCenter3D(Coord, Scale, Randomness) * 0.5;
}

