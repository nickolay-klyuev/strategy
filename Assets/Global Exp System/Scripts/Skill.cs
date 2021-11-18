public class Skill
{
    private string name;
    public string GetName()
    {
        return name;
    }

    private string desc;
    public string GetDesc()
    {
        return desc;
    }

    private string subDesc;
    private bool isUsed = false;
    public void Use()
    {
        isUsed = true;
    }
    public bool GetIsUsed()
    {
        return isUsed;
    }

    private string metaName;
    public string GetMetaName()
    {
        return metaName;
    }

    public Skill(string newName, string newDesc, string newSubDesc, string newMetaName)
    {
        name = newName;
        desc = newDesc;
        subDesc = newSubDesc;
        metaName = newMetaName;
    }

    public Skill(string newName, string newDesc, string newMetaName)
    {
        name = newName;
        desc = newDesc;
        subDesc = "";
        metaName = newMetaName;
    }
}
