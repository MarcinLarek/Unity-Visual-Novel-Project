using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Timeline;

namespace COMMANDS
{
    public class CMD_DatabaseExtension_Audio : CMD_DatabaseExtension
    {
        private static string[] PARAM_SFX = new string[] {"-s", "-sfx" };
        private static string[] PARAM_VOLUME = new string[] { "-v", "-vol", "-volume" };
        private static string[] PARAM_PITCH = new string[] { "-p", "pitch" };
        private static string[] PARAM_LOOP = new string[] { "-l", "-loop" };

        private static string[] PARAM_CHANNEL = new string[] { "-c", "-channel" };
        private static string[] PARAM_IMMEDIATE = new string[] { "-i", "-immediate" };
        private static string[] PARAM_START_VOLUME = new string[] { "-sv", "-startvolume" };
        private static string[] PARAM_SONG = new string[] { "-s", "-song" };
        private static string[] PARAM_AMBIENCE = new string[] { "-a", "-ambience" };


        new public static void Extend(CommandDatabase database)
        {
            database.AddCommand("playsfx", new Action<string[]>(PlaySFX));
            database.AddCommand("stopsfx", new Action<string>(StopSFX));

            database.AddCommand("playvoice", new Action<string[]>(PlayVoice));
            database.AddCommand("stopvoice", new Action<string>(StopSFX));

            database.AddCommand("playsong", new Action<string[]>(PlaySong));
            database.AddCommand("playambience", new Action<string[]>(PlayAmbience));

            database.AddCommand("stopsong", new Action<string>(StopSong));
            database.AddCommand("stopambience", new Action<string>(StopAmbience));

        }

        private static void PlaySFX(string[] data)
        {
            string filepath;
            float volume, ptich;
            bool loop;

            var parameters = ConvertDataToParameters(data);

            //Try to get the name or path to the sound effect
            parameters.TryGetValue(PARAM_SFX, out filepath);

            //Try to get the volume of the sound
            parameters.TryGetValue(PARAM_VOLUME, out volume, defaultValue: 1f);

            //Try to get the Ptich of the sound
            parameters.TryGetValue(PARAM_PITCH, out ptich, defaultValue: 1f);

            //Try to get if this sound loops
            parameters.TryGetValue(PARAM_LOOP, out loop, defaultValue: false);

            //Run the Logic
            AudioClip sound = Resources.Load<AudioClip>(FilePaths.GetPathToResource(FilePaths.resources_sfx, filepath));

            if (sound == null)
            {
                Debug.LogWarning($"Was not able to load sfx '{filepath}'");
                return;
            }

            AudioManager.instance.PlaySoundEffect(sound, volume: volume, pitch: ptich, loop: loop);

        }
        private static void StopSFX(string data)
        {
            AudioManager.instance.StopSoundEffect(data);
        }
        private static void PlayVoice(string[] data)
        {
            string filepath;
            float volume, ptich;
            bool loop;

            var parameters = ConvertDataToParameters(data);

            //Try to get the name or path to the sound effect
            parameters.TryGetValue(PARAM_SFX, out filepath);

            //Try to get the volume of the sound
            parameters.TryGetValue(PARAM_VOLUME, out volume, defaultValue: 1f);

            //Try to get the Ptich of the sound
            parameters.TryGetValue(PARAM_PITCH, out ptich, defaultValue: 1f);

            //Try to get if this sound loops
            parameters.TryGetValue(PARAM_LOOP, out loop, defaultValue: false);

            //Run the Logic
            AudioClip sound = Resources.Load<AudioClip>(FilePaths.GetPathToResource(FilePaths.resources_voices, filepath));

            if (sound == null)
            {
                Debug.LogWarning($"Was not able to load voice '{filepath}'");
                return;
            }
                
            AudioManager.instance.PlayVoice(sound, volume: volume, pitch: ptich, loop: loop);

        }
        private static void PlaySong(string[] data)
        {
            string filepath;
            int channel;

            var parameters = ConvertDataToParameters(data);

            //Try to get the name or path to the sound effect
            parameters.TryGetValue(PARAM_SONG, out filepath);
            filepath = FilePaths.GetPathToResource(FilePaths.resources_music, filepath);

            //Try to get the channel for this track
            parameters.TryGetValue(PARAM_CHANNEL, out channel, defaultValue: 1);

            bool ambience = false;

            PlayTrack(filepath, channel, parameters, ambience);
        }
        private static void PlayAmbience(string[] data)
        {
            string filepath;
            int channel;

            var parameters = ConvertDataToParameters(data);

            //Try to get the name or path to the sound effect
            parameters.TryGetValue(PARAM_AMBIENCE, out filepath);
            filepath = FilePaths.GetPathToResource(FilePaths.resources_ambience, filepath);

            //Try to get the channel for this track
            parameters.TryGetValue(PARAM_CHANNEL, out channel, defaultValue: 0);

            bool ambience = true;

            PlayTrack(filepath, channel, parameters, ambience);
        }
        private static void PlayTrack(string filepath, int channel, CommandParameters parameters, bool ambience)
        {
            bool loop;
            float volumeCap;
            float startVolume;
            float pitch;

            //Try to get the volume of the track
            parameters.TryGetValue(PARAM_VOLUME, out volumeCap, defaultValue: 1f);
            //Try to get the start volume of the track
            parameters.TryGetValue(PARAM_START_VOLUME, out startVolume, defaultValue: 0f);
            //Try to get the pitch of the track
            parameters.TryGetValue(PARAM_PITCH, out pitch, defaultValue: 1f);
            //Try to get if thi track loops
            parameters.TryGetValue(PARAM_LOOP, out loop, defaultValue: true);

            //Run the logic
            AudioClip sound = Resources.Load<AudioClip>(filepath);
            if(sound == null)
            {
                Debug.LogWarning($"Was not able to load track '{filepath}'");
                return;
            }

            AudioManager.instance.PlayTrack(sound, channel, loop, startVolume, volumeCap, pitch, filepath, ambience);
        }
        private static void StopSong(string data)
        {
            if (data == string.Empty)
                StopTrack("1");
            else
                StopTrack(data);
        }
        private static void StopAmbience(string data)
        {
            if (data == string.Empty)
                StopTrack("0");
            else
                StopTrack(data);
        }
        private static void StopTrack(string data)
        {
            if (int.TryParse(data, out int channel))
                AudioManager.instance.StopTrack(channel);
            else
                AudioManager.instance.StopTrack(data);
        }
    }
}