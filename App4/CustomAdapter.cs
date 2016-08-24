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
using Java.Lang;
using Java.Util.Zip;

namespace App4
{
    class CustomAdapter : BaseAdapter
    {
        Activity context;
        List<Prato> list;
        public CustomAdapter(Activity activity,List<Prato> list)
        {
            this.context = activity;
            this.list = list;
        }

        public override int Count
        {
            get
            {
                return list.Count;
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public class Holder
        {
            public TextView nome;
            public TextView valor;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            Holder holder = new Holder();

            View rowView = convertView ?? context.LayoutInflater.Inflate(
                             Resource.Layout.row_layout, parent, false);

            holder.nome = rowView.FindViewById<TextView>(Resource.Id.tvNome);
            holder.valor = rowView.FindViewById<TextView>(Resource.Id.tvValor);

            holder.nome.Text = list[position].Nome;
            holder.valor.Text = "R$"+list[position].Valor.ToString();

            return rowView;
        }
    }
}