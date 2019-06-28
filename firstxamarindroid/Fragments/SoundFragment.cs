
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using Com.Lilarcor.Cheeseknife;
using firstxamarindroid.Adapters;
using firstxamarindroid.Helpers;
using firstxamarindroid.Models;
using Realms;

namespace firstxamarindroid.SettingsModule
{
    public class SoundFragment : Fragment
    {
        [InjectView(Resource.Id.toggleSound)]
        Switch toggleSound;

        [InjectView(Resource.Id.seekBarVolume)]
        SeekBar seekBarVolume;

        [InjectView(Resource.Id.textViewVolumeLevel)]
        TextView textViewVolumeLevel;

        [InjectView(Resource.Id.recyclerViewMusic)]
        RecyclerView recyclerViewMusic;


        private SaunaModel saunaModel;
        private SoundModel soundModel;

        private SongsAdapter songsAdapter;

        private List<SongModel> songModels = new List<SongModel>()
        {
            new SongModel(1, "Song name 1", 362),
            new SongModel(2, "Song name 2", 366),
            new SongModel(3, "Song name 3", 367),
            new SongModel(4, "Song name 4", 368),
            new SongModel(5, "Song name 5", 369),
        };


        public static SoundFragment NewInstance(int saunaId)
        {
            Bundle bundle = new Bundle();
            bundle.PutInt(Helpers.Helpers.ARG_1, saunaId);

            SoundFragment soundFragment = new SoundFragment();
            soundFragment.Arguments = bundle;

            return soundFragment;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            if (Arguments != null)
            {
                int saunaId = Arguments.GetInt(Helpers.Helpers.ARG_1);

                this.saunaModel = DbController.Instance.GetSauna(saunaId);
                this.soundModel = this.saunaModel.Sound;

                this.songsAdapter = new SongsAdapter(this.songModels);
            }
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.sound_fragment, container, false);

            Cheeseknife.Inject(this, view);

            return view;
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            this.toggleSound.Checked = this.soundModel.Status;

            this.seekBarVolume.Progress = this.soundModel.Volume;
            this.seekBarVolume.ProgressChanged += SeekBarVolume_ProgressChanged;

            this.recyclerViewMusic.SetAdapter(this.songsAdapter);
            this.recyclerViewMusic.SetLayoutManager(new LinearLayoutManager(view.Context));
        }



        private void SeekBarVolume_ProgressChanged(object sender, SeekBar.ProgressChangedEventArgs e)
        {
            this.textViewVolumeLevel.Text = e.Progress.ToString();

            Realm.GetInstance().Write(() => this.soundModel.Volume = e.Progress);
        }



        [InjectOnClick(Resource.Id.toggleSound)]
        private void ToggleSound_Click(object sender, EventArgs e)
        {
            Realm.GetInstance().Write(() => this.soundModel.Status = this.toggleSound.Checked);
        }
    }
}
