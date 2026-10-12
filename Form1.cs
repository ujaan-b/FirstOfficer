using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
// C#
using Microsoft.FlightSimulator.SimConnect;
using System.Runtime.InteropServices;
using OpenAI.Responses;
#pragma warning disable OPENAI001
using System.Speech.Synthesis;
using System.Speech.Recognition;
using OpenAI.Audio;
using NAudio.Wave;



namespace FirstOfficer
{
    public partial class Form1 : Form   // Form
    {
        private SimConnect simconnect = null;   
        private const int WM_USER_SIMCONNECT = 0X0402;
        public double aiAltitude = 0.0;
        private SpeechSynthesizer speechSynth = new SpeechSynthesizer();

       // private SpeechRecognitionEngine speechRecog = new SpeechRecognitionEngine();
        private SpeechRecognitionEngine speechRecog = new SpeechRecognitionEngine();

        //declaring audio recorder files
        private WaveInEvent waveIn;
        private WaveFileWriter waveWriter;
        private string recordAudioPath;

        private string apiKey;


        public Form1()  // Initialize the form
        {
            InitializeComponent();
            this.Load += Form1_Load;

        }

        private void Form1_Load(object sender, EventArgs e) // What the form will do when loading
        {
            //throw new NotImplementedException();
            try
            {
                // Enable voice inputs (microphone)
                speechRecog.SetInputToDefaultAudioDevice();
                // Enable grammar of speech input
                /*speechRecog.LoadGrammar(new DictationGrammar());
                speechRecog.SpeechRecognized += SpeechRecog_SpeechRecognized;   // Windows microphone will continue listening for multiple words
                speechRecog.RecognizeAsync(RecognizeMode.Multiple);*/

                // Connecting to the sim
                simconnect = new SimConnect("FirstOfficer", this.Handle, WM_USER_SIMCONNECT, null, 0); // Initiate the sim connection
                simconnect.OnRecvOpen += Simconnect_OnRecvOpen; // Open the sim connection
                apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");

            }
            catch (COMException)
            {
                MessageBox.Show("Unable to connect to flight sim");
            }

        }

        /*private void SpeechRecog_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            throw new NotImplementedException();
        }*/

        // What to do once it opens 
        private void Simconnect_OnRecvOpen(SimConnect sender, SIMCONNECT_RECV_OPEN data)  
        {
            
            //throw new NotImplementedException();
            MessageBox.Show("Connected succesfully to flight sim");
           
            simconnect.AddToDataDefinition // Define the simconnect altitude data 
            (
                DEFINITIONS.AircraftData, "Plane Altitude", "feet", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED // Define simconnect altitude data
            ); 
           
           

            simconnect.RegisterDataDefineStruct<AircraftData>
            (
                DEFINITIONS.AircraftData
            );
            simconnect.OnRecvSimobjectData += SimConnect_OnRecvSimobjectData;
            
            simconnect.RequestDataOnSimObject(DATA_REQUESTS.AltitudeRequest, DEFINITIONS.AircraftData, SimConnect.SIMCONNECT_OBJECT_ID_USER,
                SIMCONNECT_PERIOD.SECOND, SIMCONNECT_DATA_REQUEST_FLAG.DEFAULT, 0, 0, 0);
        }

        protected override void WndProc(ref Message m)  // Create the dialogue box
        {
            if (m.Msg == WM_USER_SIMCONNECT && simconnect != null)
            {
                simconnect.ReceiveMessage();
            }
            base.WndProc(ref m);
        }

        enum DEFINITIONS {AircraftData}
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        struct AircraftData
        {
            public double altitude;
        }

        enum DATA_REQUESTS {AltitudeRequest}

        private void SimConnect_OnRecvSimobjectData(SimConnect sender, SIMCONNECT_RECV_SIMOBJECT_DATA data) // Read the Altitude value
        {
            if (data.dwRequestID == (uint) DATA_REQUESTS.AltitudeRequest)
            {
                AircraftData aircraftData = (AircraftData)data.dwData[0];
                aiAltitude = aircraftData.altitude;
                //MessageBox.Show("Current Altitude: " + aircraftData.altitude.ToString("F0") + " ft");
                lblAltitude.Text = "Altitude: " + aircraftData.altitude.ToString("F0") + " ft";

            }
        }

        private void Form1_Load_1(object sender, EventArgs e)

        {

        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private async void btnTestAI_Click(object sender, EventArgs e)
        {
            // Check if I can access the environment variable
            // Async is used to make OpenAI asynchronous call in future

            try
            {

                
                if (apiKey != null)
                {
                    //MessageBox.Show("API Key successfully found");

                    // Create an OpenAI Responce Client
                    ResponsesClient client = new ResponsesClient(apiKey: apiKey);

                    // Build a prompt for the Altitude
                    // string prompt = "You are a Flight Assistant. The current altitude is " + aiAltitude.ToString("F0") + " ft";

                    // Create a user given prompt
                    string userQuestion = txtQuestion.Text;
                    string prompt = "You are a Flight Assistant. The current altitude is " + aiAltitude.ToString("F0") + " ft" + "answer the following pilot question using the aircraft data provided: " + userQuestion;


                    // Send send prompt to Luna
                    ResponseResult response = await client.CreateResponseAsync("gpt-5.6-luna", prompt);
                    string aiResponse = response.GetOutputText();

                    // Show success message
                    //MessageBox.Show(response.GetOutputText());
                    lblAIResponse.Text = aiResponse;
                    speechSynth.SpeakAsync(aiResponse);
                    


                }
                else
                {
                    MessageBox.Show("API Key not found");
                    
                }

                

            }
            catch (Exception ex)
            {
                MessageBox.Show("I got the Error - " + ex.Message);
            }


            
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            
        }

        private void SpeechRecog_SpeechRecognized(object sender, SpeechRecognizedEventArgs e) 
        {
            txtQuestion.Text = e.Result.Text;
        }

        private void btnRecordVoice_Click(object sender, EventArgs e)
        {
            recordAudioPath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "FirstOfficerVoice.wav");
            waveIn = new WaveInEvent();
            waveIn.WaveFormat = new WaveFormat(16000, 1);
            waveWriter = new WaveFileWriter(recordAudioPath, waveIn.WaveFormat);
            waveIn.DataAvailable += (s, a) =>
            {
                waveWriter.Write(a.Buffer, 0, a.BytesRecorded);
            };
            waveIn.StartRecording();
            btnRecordVoice.Text = "Recording...";
        }

        private void btnStopRecord_Click(object sender, EventArgs e)
        {
            waveIn.StopRecording();
            if (waveWriter != null)
            {
                waveWriter.Dispose();
                waveWriter = null;
            }

            if (waveIn != null)
            {
                waveIn.Dispose();
                waveIn = null;
            }

            btnRecordVoice.Text = "Record Voice";
            MessageBox.Show("Recording completed");
            
            System.Diagnostics.Process.Start(recordAudioPath);
        }

        private async void btnTranscribe_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(apiKey))
                {
                    MessageBox.Show("API Key not found");
                    return;
                }

                if (string.IsNullOrEmpty(recordAudioPath) || !System.IO.File.Exists(recordAudioPath))
                {
                    MessageBox.Show(".wav File not found");
                    return;
                }

                AudioClient audioClient = new AudioClient(model: "gpt-4o-transcribe", apiKey: apiKey);
                AudioTranscription audioTrans = await audioClient.TranscribeAudioAsync(recordAudioPath);
                txtQuestion.Text = audioTrans.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Transcription Error: " + ex.Message);
            }
        }
    }
}
// Callback: 
// Struct:
// AI Model Comparison
// API Key Acquired
// API Secret Key stored in Windows environment
// AI answers prompts correcty:
// * If question asked is outside flight sim, uses other AI knowledge to answers:"why dont cats wear diapers"
// * If input is gibberish (ex: "hfjuoeqhfgoiq") answers that its unable to answer and still gives altitude value
// Next step: Enabling voice response
// Voice response actually works
// System.Recognizer speech recognition is very inaccurate
// * We tested different microphones and input volume levels
// * Will try OpenAI.Audio.Audioclient including with GPT-4o-Transcribe
// Installed Audio recording Library so that the voice can be captured from the microphone into a .wav file
// Got Issue installing NAudio version 3.1.0 (not compatible with .Net SDK being used), had to download 2.2.1 instead
// Created .wav file in Windows Temporary path to record speech by OpenAI audio
// Added transcribe, windows continues to record even after the AI transcribes the .wav file
