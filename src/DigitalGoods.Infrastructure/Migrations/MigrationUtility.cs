namespace DigitalGoods.Infrastructure.Migrations
{
    internal static class MigrationUtility
    {
        public static string ReadSql(Type migrationType, string sqlFileName)
        {
            var assembly = migrationType.Assembly;
            string resourceName = $"{migrationType.Namespace}.{sqlFileName}";
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    throw new FileNotFoundException("Unable to find the SQL file from an embedded resource", 
                        resourceName);
                }

                using (var reader = new StreamReader(stream))
                {
                    var content = reader.ReadToEnd();
                    return content;
                }
            }
        }
    }
}
