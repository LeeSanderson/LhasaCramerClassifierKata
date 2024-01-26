namespace CramerClassification;

public record Text(Colour Colour, string Value, double Opacity = 1): FunctionalGroup(Colour, Opacity);