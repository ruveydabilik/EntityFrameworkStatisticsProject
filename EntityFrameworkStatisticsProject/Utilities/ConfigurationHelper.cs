using System.Configuration;
using System.Xml;

public static class ConnectionStringHelper
{
    public static string GetConnectionString(string name)
    {
        XmlDocument configDoc = new XmlDocument();
        configDoc.Load("connection.config");

        XmlNode node = configDoc.SelectSingleNode($"//connectionStrings/add[@name='{name}']");
        if (node != null && node.Attributes["connectionString"] != null)
        {
            return node.Attributes["connectionString"].Value;
        }
        else
        {
            throw new ConfigurationErrorsException($"Connection string '{name}' not found.");
        }
    }
}
