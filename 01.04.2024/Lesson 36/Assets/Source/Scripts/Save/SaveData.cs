public class SaveData
{
    public SaveData(string id, string dataType)
    {
        ID = id;
        DataType = dataType;
    }

    public string ID { get; set; }
    public string DataType { get; set; }
}