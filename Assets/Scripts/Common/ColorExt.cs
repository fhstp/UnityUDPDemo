using UnityEngine;

namespace Ac.At.FhStp.UnityUDPDemo
{

    public static class ColorExt
    {

        public static Color WithA(this Color c, float a) =>
            new Color(c.r, c.g, c.g, a);

    }

}