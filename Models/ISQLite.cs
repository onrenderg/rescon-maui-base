
using SQLite;

namespace ResillentConstruction
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
