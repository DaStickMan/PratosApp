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
using SQLite;

namespace App4
{
    public class Prato
    {
        [PrimaryKey, AutoIncrement]
        public long ID { get; set; }

        public string Nome { get; set; }

        public Double Valor { get; set; }

        public override string ToString()
        {
            return Nome;
        }
    }
}