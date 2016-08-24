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
using System.Globalization;

namespace App4
{
    [Activity(Theme = "@android:style/Theme.Holo.Light",Label = "Adicionar", Icon = "@android:color/transparent")]
    public class Adicionar : Activity
    {
        Button b;
        EditText nome;
        EditText valor;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Adicionar);

            b = FindViewById<Button>(Resource.Id.button1);
            nome = FindViewById<EditText>(Resource.Id.editText1);
            valor = FindViewById<EditText>(Resource.Id.editText2);

            ActionBar.SetHomeButtonEnabled(true);
            ActionBar.SetDisplayHomeAsUpEnabled(true);

            b.Click += (object sender, EventArgs e) =>
            {
                Prato prat = new Prato();

                prat.Nome = nome.Text;

                if (valor.Text != "")
                {
                    prat.Valor = double.Parse(valor.Text, CultureInfo.InvariantCulture);
                    //TESTS
                }else
                {
                    prat.Valor = 0.0;
                }

                PratoDAO dao = new PratoDAO(this);

                dao.cadastrar(prat);

                dao.Close();

                Finish();
            };

            
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    Finish();
                    return true;

                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

    }
}