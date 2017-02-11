using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Dimension
{
    static int dimension;
    public static bool is2D()
    {
        return dimension == 2;
    }

    public static bool is3D()
    {
        return dimension == 3;
    }
    public static void set2D()
    {
        dimension = 2;
    }

    public static void set3D()
    {
        dimension = 3;
    }
}
