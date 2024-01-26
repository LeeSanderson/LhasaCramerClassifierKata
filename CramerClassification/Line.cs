namespace CramerClassification;

public record Line(Colour Colour, int X1, int Y1, int X2, int Y2, double Opacity = 1) : FunctionalGroup(Colour, Opacity)
{
    public int Length => (int)Math.Sqrt(Math.Pow(X2 - X1, 2) + Math.Pow(Y2 - Y1, 2));
}