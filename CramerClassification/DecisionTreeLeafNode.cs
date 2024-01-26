namespace CramerClassification;

public record DecisionTreeLeafNode(ToxicityRisk ToxicityRisk): DecisionTreeNode
{
    public override ToxicityRisk Evaluate(Chemical chemical) => ToxicityRisk;
}