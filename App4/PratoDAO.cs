using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Database.Sqlite;
using Android.Util;
using Android.Database;

namespace App4
{
    class PratoDAO : SQLiteOpenHelper
    {
        private static string DATABASE_DIRECTORY = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        private static string DATABASE_FILE_NAME = "Spotlight.sqlite";
        private static string TABELA = "Pratos";
        private static string TAG = "CADASTRO";

        private SQLiteDatabase db;

        private static int VERSION = 1;
        private Context context = null;

        public PratoDAO(Context _context) : base(_context, DATABASE_FILE_NAME, null, VERSION)
        {
            this.context = _context;
        }
        public override void OnCreate(SQLiteDatabase db)
        {
            String ddl = "CREATE TABLE " + TABELA + "("
                + "id INTEGER PRIMARY KEY, "
                + "nome TEXT, valor REAL)";
            

            db.ExecSQL(ddl);

            this.db = db;

        }

        public override void OnUpgrade(SQLiteDatabase db, int oldVersion, int newVersion)
        {
            String sql = "DROP TABLE IF EXISTS " + TABELA;

            db.ExecSQL(sql);

            OnCreate(db);
        }
        public void cadastrar(Prato prat)
        {

            ContentValues values = new ContentValues();

            

            values.Put("nome", prat.Nome);
            values.Put("valor", prat.Valor);


            WritableDatabase.Insert(TABELA, null, values);

            Log.Info(TAG, "Prato cadastradado: " + prat.Nome);
        }
        public void deletar(Prato prat)
        {
            String[] args = { prat.ID.ToString() };

            WritableDatabase.Delete(TABELA, "id=?", args);


        }

        public List<Prato> listar()
        {
            List<Prato> lista = new List<Prato>();

            String sql = "Select * from " + TABELA + " ORDER BY nome";

            ICursor cursor = ReadableDatabase.RawQuery(sql, null);

            try
            {
                while (cursor.MoveToNext())
                {
                    Prato prat = new Prato();

                    prat.ID = cursor.GetLong(0);
                    prat.Nome = cursor.GetString(1);
                    prat.Valor = cursor.GetDouble(2);
                    lista.Add(prat);
                }

            }
            catch (SQLException e)
            {
                Log.Error(TAG, e.Message);
            }
            finally
            {
                cursor.Close();
            }

            return lista;

        }
    }
}