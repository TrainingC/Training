using UnityEngine;
using System.Collections;

enum TurnState
{
    Idel,
    Player,
    Enemy,
}

enum RayState
{
    Idel,
    Move,
    Hit,
    Decision,
}

enum AppearanceState
{
    Idel,
    Operate,
}

public enum Height         //高さ
{
    Small,
    Medium,
    Large,
}

public enum Width          //表紙の幅
{
    Small,
    Medium,
    Large,
}

public enum Thickness      //厚さ
{
    Small,
    Medium,
    Large,
}

public enum Strengthening
{
    None,
    Small,
    Medium,
    Large,
    MagicalPower,
}

public enum Magic          //魔法陣の型
{
    None,
    DefenseDebuff,
}

public enum Chapter        //上・中・下
{
    None,
    Lower,
    Middle,
    Upper,
}

public enum Books
{
    Small,
    SmallStr,
    Medium,
    MediumStr,
    MediumMPStr,
    Large,
    Large2,
}

public struct BooksType
{
    public bool m_Aggression;
    public Height m_HeightDivide;
    public Width m_WidthDivide;
    public Thickness m_ThicknessDivide;
    public Strengthening m_StrengtheningDivide;
    public Magic m_MagicDivide;
    public Chapter m_ChapterDivide;
    public Books m_Books;

    private BooksType(bool Aggression, Height HeightDivide, Width WidthDivide, Thickness ThicknessDivide, Books Books)
    {
        m_Aggression = Aggression;
        m_HeightDivide = HeightDivide;
        m_WidthDivide = WidthDivide;
        m_ThicknessDivide = ThicknessDivide;
        m_StrengtheningDivide = Strengthening.None;
        m_MagicDivide = Magic.None;
        m_ChapterDivide = Chapter.None;
        m_Books = Books;
    }

    private BooksType(bool Aggression, Height HeightDivide, Width WidthDivide,
        Thickness ThicknessDivide, Strengthening StrengtheningDivide, Magic MagicDivide, Chapter ChapterDivide, Books Books)
    {
        m_Aggression = Aggression;
        m_HeightDivide = HeightDivide;
        m_WidthDivide = WidthDivide;
        m_ThicknessDivide = ThicknessDivide;
        m_StrengtheningDivide = StrengtheningDivide;
        m_MagicDivide = MagicDivide;
        m_ChapterDivide = ChapterDivide;
        m_Books = Books;
    }

    public static BooksType Small()
    {
        return new BooksType(true, Height.Small, Width.Small, Thickness.Small, Books.Small);
    }

    public static BooksType SmallStr()
    {
        return new BooksType(false, Height.Small, Width.Small, Thickness.Small, Strengthening.Small, Magic.None, Chapter.None, Books.SmallStr);
    }

    public static BooksType Medium()
    {
        return new BooksType(true, Height.Medium, Width.Medium, Thickness.Medium, Books.Medium);
    }

    public static BooksType MediumStr()
    {
        return new BooksType(false, Height.Medium, Width.Medium, Thickness.Medium, Strengthening.Medium, Magic.None, Chapter.None, Books.MediumStr);
    }

    public static BooksType MediumMPStr()
    {
        return new BooksType(false, Height.Medium, Width.Medium, Thickness.Medium, Strengthening.MagicalPower, Magic.None, Chapter.None, Books.MediumMPStr);
    }

    public static BooksType Large()
    {
        return new BooksType(true, Height.Large, Width.Medium, Thickness.Large, Books.Large);
    }

    public static BooksType Large2()
    {
        return new BooksType(true, Height.Large, Width.Large, Thickness.Large, Books.Large2);
    }
}