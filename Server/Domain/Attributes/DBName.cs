[System.AttributeUsage(System.AttributeTargets.Property | System.AttributeTargets.Class)]
public class DBName : System.Attribute
{
    private string _dbName;
    public DBName(string dbName)
    {
        DbName = dbName;
    }

    public string DbName { get => _dbName; set => _dbName = value; }
}