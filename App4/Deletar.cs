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
using Android.Util;

namespace App4.Resources.layout
{
    [Activity(Theme = "@android:style/Theme.Holo.Light",Label = "Deletar" ,Icon = "@android:color/transparent")]
    public class Deletar : Activity
    {

        Prato pratoAtual;
        List<Prato> list;

        public ArrayAdapter<Prato> ListAdapter { get; private set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Deletar);

            ActionBar.SetHomeButtonEnabled(true);
            ActionBar.SetDisplayHomeAsUpEnabled(true);

            CarregarLista();
        }

        private void CarregarLista()
        {
            PratoDAO prat = new PratoDAO(this);

            list = prat.listar();

            ListAdapter = new ArrayAdapter<Prato>(this, Android.Resource.Layout.SelectDialogMultiChoice, list);

            ListView lstPratos = FindViewById<ListView>(Resource.Id.lstDeletar);

            lstPratos.ChoiceMode = Android.Widget.ChoiceMode.Multiple;



            lstPratos.Adapter = ListAdapter;
        }

        private void varrerLista()
        {
            PratoDAO dao = new PratoDAO(this);
            var sparseArray = FindViewById<ListView>(Resource.Id.lstDeletar).CheckedItemPositions;
            for (var i = 0; i < sparseArray.Size(); i++)
            {
                
                if (sparseArray.ValueAt(i))
                {


                    Log.Info("DELETAR", "INDEX = "+i +" KEY=" +sparseArray.KeyAt(i) + "=" + sparseArray.ValueAt(i));
                    dao.deletar(list[sparseArray.KeyAt(i)]);
                }

                Console.Write(sparseArray.KeyAt(i) + "=" + sparseArray.ValueAt(i) + ",");
            }
            Console.WriteLine();
            
            dao.Close();

        }

        protected override void OnResume()
        {
            base.OnResume();
            CarregarLista();
        }

        public override bool OnPrepareOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_deletar, menu);
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
               
                case Resource.Id.action_delete:

                    Vibrator vibe = (Vibrator)this.GetSystemService(Context.VibratorService);

                    vibe.Vibrate(50);

                    new AlertDialog.Builder(this).
                        SetIcon(Android.Resource.Drawable.IcDialogAlert).
                        SetTitle("Deletar pratos").
                        SetMessage("Tem certeza que deseja deletar?").
                        SetPositiveButton("Sim", (senderAlert, args) =>
                        {
                            
                            varrerLista();
                            CarregarLista();

                            Finish();

                            Toast.MakeText(this,"Pratos deletados com sucesso!",ToastLength.Short).Show();

                        }).SetNegativeButton("Não", (senderAlert, args) => {

                        }).Show();

                    return true;
                case Android.Resource.Id.Home:
                    Finish();
                    return true;

                default:
                    return false;
            }
            return OnOptionsItemSelected(item);
        }
    }
}