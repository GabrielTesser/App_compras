using System;
using System.Collections.Generic;
using System.Text;
using compras.Model;
using System.Threading.Tasks;
using SQLite;
using compras.Model;

namespace compras.Helper
{
    public class SQLiteDatabaseHelper
    {

       readonly SQLiteAsyncConnection _conn;

        public SQLiteDatabaseHelper(string path)
        {
            _conn = new SQLiteAsyncConnection(path);
            _conn.CreateTableAsync<Produto>().Wait();
        }

        public  Task<int> Insert(Produto p)
        {
            return _conn.InsertAsync(p);
        }

        public void Update(Produto p)
        {
            string sql = "UPDATE Produto SET Descricao=?, Quantidade=?, Preco=? WHERE Id=?";

            _conn.QueryAsync<Produto>(sql, p.Descricao, p.Quantidade, p.Preco, p.Id);
        }

        public Task<List<Produto>> getAll()
        {
            return _conn.Table<Produto>().ToListAsync();
        }

        public void delete(int id)
        {
            _conn.Table<Produto>().DeleteAsync(i => i.Id == id);
        }

    }
}
