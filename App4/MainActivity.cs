using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Util;
using System.Collections.Generic;
using App4.Resources.layout;

namespace App4
{
    [Activity(Theme = "@android:style/Theme.Holo.Light", Label = "Pratos")]
    public class MainActivity : Activity
    {

        public ArrayAdapter<Prato> ListAdapter { get; private set; }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            CarregarLista();
        }

        private void CarregarLista()
        {
            PratoDAO prat = new PratoDAO(this);

            List<Prato> list = prat.listar();
            
            ListView lstPratos = FindViewById<ListView>(Resource.Id.lstPratos);

            lstPratos.Adapter = new CustomAdapter(this, list);
            
        }

        public override bool OnPrepareOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu, menu);
            return base.OnPrepareOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {

            int id = item.ItemId;
            if (id == Resource.Id.action_bar)
            {
                return true;
            }
            switch (item.ItemId)
            {
                case Resource.Id.action_add:
                    var intent = new Intent(Android.App.Application.Context, typeof(Adicionar));
                    intent.SetFlags(ActivityFlags.NewTask);
                    Android.App.Application.Context.StartActivity(intent);
                    return true;
                case Resource.Id.action_delete:
                    intent = new Intent(Android.App.Application.Context, typeof(Deletar));
                    intent.SetFlags(ActivityFlags.NewTask);
                    Android.App.Application.Context.StartActivity(intent);
                    return true;

                default:
                    return false;
            }
            return OnOptionsItemSelected(item);
        }


        protected override void OnResume()
        {
            base.OnResume();
            CarregarLista();
        }
    }
   
}

