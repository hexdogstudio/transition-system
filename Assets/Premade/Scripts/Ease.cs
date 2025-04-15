using UnityEngine;

public static class Ease
{
    public enum Type
    {
        Linear,
        InExpo,
        OutExpo,
        InOutExpo,
        InBack,
        OutBack,
        InOutBack,
        InSine,
        OutSine,
        InOutSine,
        InCubic,
        OutCubic,
        InOutCubic,
        InQuint,
        OutQuint,
        InOutQuint,
        InCirc,
        OutCirc,
        InOutCirc,
        InElastic,
        OutElastic,
        InOutElastic,
        InQuad,
        OutQuad,
        InOutQuad,
        InQuart,
        OutQuart,
        InOutQuart
    }

    public static float Func(float x, Type type)
    {
        return type switch
        {
            Type.InExpo => InExpo(x),
            Type.OutExpo => OutExpo(x),
            Type.InOutExpo => InOutExpo(x),
            Type.InBack => InBack(x),
            Type.OutBack => OutBack(x),
            Type.InOutBack => InOutBack(x),
            Type.InSine => InSine(x),
            Type.OutSine => OutSine(x),
            Type.InOutSine => InOutSine(x),
            Type.InCubic => InCubic(x),
            Type.OutCubic => OutCubic(x),
            Type.InOutCubic => InOutCubic(x),
            Type.InQuint => InQuint(x),
            Type.OutQuint => OutQuint(x),
            Type.InOutQuint => InOutQuint(x),
            Type.InCirc => InCirc(x),
            Type.OutCirc => OutCirc(x),
            Type.InOutCirc => InOutCirc(x),
            Type.InElastic => InElastic(x),
            Type.OutElastic => OutElastic(x),
            Type.InOutElastic => InOutElastic(x),
            Type.InQuad => InQuad(x),
            Type.OutQuad => OutQuad(x),
            Type.InOutQuad => InOutQuad(x),
            Type.InQuart => InQuart(x),
            Type.OutQuart => OutQuart(x),
            Type.InOutQuart => InOutQuart(x),
            _ => x,
        };
    }

    public static System.Func<float, float> Func(Type type)
    {
        return type switch
        {
            Type.InExpo => InExpo,
            Type.OutExpo => OutExpo,
            Type.InOutExpo => InOutExpo,
            Type.InBack => InBack,
            Type.OutBack => OutBack,
            Type.InOutBack => InOutBack,
            Type.InSine => InSine,
            Type.OutSine => OutSine,
            Type.InOutSine => InOutSine,
            Type.InCubic => InCubic,
            Type.OutCubic => OutCubic,
            Type.InOutCubic => InOutCubic,
            Type.InQuint => InQuint,
            Type.OutQuint => OutQuint,
            Type.InOutQuint => InOutQuint,
            Type.InCirc => InCirc,
            Type.OutCirc => OutCirc,
            Type.InOutCirc => InOutCirc,
            Type.InElastic => InElastic,
            Type.OutElastic => OutElastic,
            Type.InOutElastic => InOutElastic,
            Type.InQuad => InQuad,
            Type.OutQuad => OutQuad,
            Type.InOutQuad => InOutQuad,
            Type.InQuart => InQuart,
            Type.OutQuart => OutQuart,
            Type.InOutQuart => InOutQuart,
            _ => (x) => x,
        };
    }

    //Expo
    public static float InOutExpo(float x)
    {
        return x == 0
        ? 0
        : x == 1
        ? 1
        : x < 0.5 ? Mathf.Pow(2, 20 * x - 10) / 2
        : (2 - Mathf.Pow(2, -20 * x + 10)) / 2;
    }
    public static float OutExpo(float x)
    {
        return x == 1 ? 1 : 1 - Mathf.Pow(2, -10 * x);
    }
    public static float InExpo(float x)
    {
        return x == 0 ? 0 : Mathf.Pow(2, 10 * x - 10);
    }

    //Back
    public static float InBack(float x)
    {
        float c1 = 1.70158f;
        float c3 = c1 + 1;

        return c3 * x * x * x - c1 * x * x;
    }
    public static float OutBack(float x)
    {
        float c1 = 1.70158f;
        float c3 = c1 + 1;

        return 1 + c3 * Mathf.Pow(x - 1, 3) + c1 * Mathf.Pow(x - 1, 2);
    }
    public static float InOutBack(float x)
    {
        float c1 = 1.70158f;
        float c2 = c1 * 1.525f;

        return x < 0.5
          ? (Mathf.Pow(2 * x, 2) * ((c2 + 1) * 2 * x - c2)) / 2
          : (Mathf.Pow(2 * x - 2, 2) * ((c2 + 1) * (x * 2 - 2) + c2) + 2) / 2;
    }

    //Sine
    public static float InSine(float x)
    {
        return 1 - Mathf.Cos((x * Mathf.PI) / 2);
    }
    public static float OutSine(float x)
    {
        return Mathf.Sin((x * Mathf.PI) / 2);
    }
    public static float InOutSine(float x)
    {
        return -(Mathf.Cos(Mathf.PI * x) - 1) / 2;
    }

    //Cubic
    public static float InCubic(float x)
    {
        return x * x * x;
    }
    public static float OutCubic(float x)
    {
        return 1 - Mathf.Pow(1 - x, 3);
    }
    public static float InOutCubic(float x)
    {
        return x < 0.5 ? 4 * x * x * x : 1 - Mathf.Pow(-2 * x + 2, 3) / 2;
    }

    //Quint
    public static float InQuint(float x)
    {
        return x * x * x * x * x;
    }
    public static float OutQuint(float x)
    {
        return 1 - Mathf.Pow(1 - x, 5);
    }
    public static float InOutQuint(float x)
    {
        return x < 0.5 ? 16 * x * x * x * x * x : 1 - Mathf.Pow(-2 * x + 2, 5) / 2;
    }

    //Circ
    public static float InCirc(float x)
    {
        return 1 - Mathf.Sqrt(1 - Mathf.Pow(x, 2));
    }
    public static float OutCirc(float x)
    {
        return Mathf.Sqrt(1 - Mathf.Pow(x - 1, 2));
    }
    public static float InOutCirc(float x)
    {
        return x < 0.5
        ? (1 - Mathf.Sqrt(1 - Mathf.Pow(2 * x, 2))) / 2
        : (Mathf.Sqrt(1 - Mathf.Pow(-2 * x + 2, 2)) + 1) / 2;
    }

    //Elastic
    public static float InElastic(float x)
    {
        float c4 = (2 * Mathf.PI) / 3;

        return x == 0
          ? 0
          : x == 1
          ? 1
          : -Mathf.Pow(2, 10 * x - 10) * Mathf.Sin((x * 10 - 10.75f) * c4);
    }
    public static float OutElastic(float x)
    {
        float c4 = (2 * Mathf.PI) / 3;

        return x == 0
          ? 0
          : x == 1
          ? 1
          : Mathf.Pow(2, -10 * x) * Mathf.Sin((x * 10 - 0.75f) * c4) + 1; ;
    }
    public static float InOutElastic(float x)
    {
        float c5 = (2 * Mathf.PI) / 4.5f;

        return x == 0
          ? 0
          : x == 1
          ? 1
          : x < 0.5f
          ? -(Mathf.Pow(2, 20 * x - 10) * Mathf.Sin((20 * x - 11.125f) * c5)) / 2
          : (Mathf.Pow(2, -20 * x + 10) * Mathf.Sin((20 * x - 11.125f) * c5)) / 2 + 1;
    }

    //Quad
    public static float InQuad(float x)
    {
        return x * x;
    }
    public static float OutQuad(float x)
    {
        return 1 - (1 - x) * (1 - x);
    }
    public static float InOutQuad(float x)
    {
        return x < 0.5 ? 2 * x * x : 1 - Mathf.Pow(-2 * x + 2, 2) / 2;
    }

    //Quart
    public static float InQuart(float x)
    {
        return x * x * x * x;
    }
    public static float OutQuart(float x)
    {
        return 1 - Mathf.Pow(1 - x, 4);
    }
    public static float InOutQuart(float x)
    {
        return x < 0.5 ? 8 * x * x * x * x : 1 - Mathf.Pow(-2 * x + 2, 4) / 2;
    }

    //Bounce
    public static float InBounce(float x)
    {
        return 1 - OutBounce(1 - x);
    }
    public static float OutBounce(float x)
    {
        float n1 = 7.5625f;
        float d1 = 2.75f;

        if (x < 1 / d1)
        {
            return n1 * x * x;
        }
        else if (x < 2 / d1)
        {
            return n1 * (x -= 1.5f / d1) * x + 0.75f;
        }
        else if (x < 2.5 / d1)
        {
            return n1 * (x -= 2.25f / d1) * x + 0.9375f;
        }
        else
        {
            return n1 * (x -= 2.625f / d1) * x + 0.984375f;
        }
    }
    public static float InOutBounce(float x)
    {
        return x < 0.5f
        ? (1 - OutBounce(1 - 2 * x)) / 2
        : (1 + OutBounce(2 * x - 1)) / 2;
    }
}