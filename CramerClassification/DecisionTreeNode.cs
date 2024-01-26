namespace CramerClassification;

public abstract record DecisionTreeNode
{
    protected static readonly DecisionTreeNode LowRisk = new DecisionTreeLeafNode(ToxicityRisk.Low);
    protected static readonly DecisionTreeNode MediumRisk = new DecisionTreeLeafNode(ToxicityRisk.Medium);
    protected static readonly DecisionTreeNode HighRisk = new DecisionTreeLeafNode(ToxicityRisk.High);
    
    public abstract ToxicityRisk Evaluate(Chemical chemical);
}