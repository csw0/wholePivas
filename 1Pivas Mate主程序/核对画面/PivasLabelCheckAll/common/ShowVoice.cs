using SpeechLib;
using System;

namespace PivasLabelCheckAll.common
{
    public  class ShowVoice
    {
        public  string Speak(string speak)
        {
            try
            {
                //char[] chars=speak.ToArray();
                SpVoice voice = new SpVoice();
                voice.Rate = -1;//朗读速度
                voice.Voice = voice.GetVoices(string.Empty, string.Empty).Item(0);
                //for (int i = 0; i < chars.Length; i++)
                //{
                //    voice.Speak(chars[i].ToString(), SpeechVoiceSpeakFlags.SVSFDefault); 
                //}
                voice.Speak(speak, SpeechVoiceSpeakFlags.SVSFDefault);
                //voice.Speak("完毕", SpeechVoiceSpeakFlags.SVSFDefault); 
                return "";
            }
            catch (Exception ex)
            {
               return "请检测音频设备及语音包是否正常！";
                
            }
            
        }
    }
}
