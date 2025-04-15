using System;
using UnityEngine;

[Serializable]
public class EaseCurve
{
    [SerializeField] private Ease.Type m_Type;
    [NonSerialized] private Func<float, float> m_Func;
#if UNITY_EDITOR
    [SerializeField, HideInInspector] private int m_Hash;
#endif

    private Func<float, float> func
    {
        get
        {
#if UNITY_EDITOR
            int hash = m_Type.GetHashCode();
            if (m_Hash != hash || m_Func == null)
            {
                m_Func = Ease.Func(m_Type);
                m_Hash = hash;
            }
#else
            m_Func ??= Ease.Func(m_Type);
#endif
            return m_Func;
        }
    }

    public Ease.Type type
    {
        get => m_Type; set
        {
            m_Type = value;
            m_Func = Ease.Func(m_Type);
        }
    }

    public EaseCurve(Ease.Type type)
    {
        m_Type = type;
        m_Func = Ease.Func(type);
    }

    public float Calc(float x)
    {
        return func.Invoke(x);
    }
    public override string ToString()
    {
        return $"EaseCurve ({m_Type})";
    }
}
