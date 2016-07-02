using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using OpenTK;
using OpenTK.Graphics;

namespace Taco.Classes
{
    class SolarSystem
    {
        #region Private Members

        public static Color DefaultDrawColor = Color.FromArgb(255, 172, 207, 243);
        public static Color HighlightDrawColor = Color.White;
        public static Color AlertingDrawColor = Color.Red;

        //private Color4 _drawColorC4;

        private Regex _nameRegex;
        #endregion Private Members

        public SolarSystem(int nativeId, string name, double x, double y, double z)
        {
            // Setup Data
            NativeId = nativeId;
            Name = name;
            X = x;
            Y = y;
            Z = z;

            // Setup base "cached" derived
            var xf = (float)X;
            var yf = (float)Y;
            var zf = (float)Z;

            // Setup flag defaults
            IsHighlighted = false;
            IsAlerting = false;
            IsSelected = false;

            // Setup animation defaults
            AlertState = AnimationState.Idle;
            CurrentStep = 1;
            DrawSize = 0.0f;
            DrawColor = DefaultDrawColor;

            // Setup animation "cached" derived
            //_drawColorC4 = new Color4(DrawColor.R, DrawColor.G, DrawColor.B, DrawColor.A);

            Xyz = new Vector3(xf, yf, zf);

            _alertPulseCount = 0;

            string regexName = ""; 
            foreach(char letter in name)
            {
                if (Char.IsLetter(letter))
                {
                    regexName += "[";
                    regexName += letter.ToString().ToLower();
                    regexName += letter.ToString().ToUpper();
                    regexName += "]";
                }
                else
                {
                    regexName += letter;
                }
            }

            _nameRegex = new Regex(@"\b" + regexName + @"\b", RegexOptions.Compiled);
        }

        #region Base Properties
        // Base Properties
        public int NativeId { get; set; }
        public string Name { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public List<SolarSystemConnection> ConnectedTo { get; set; }

        // Derived Base Properties
        public float Xf
        {
            get
            {
                return (float)X;
            }
        }

        public float Yf
        {
            get
            {
                return (float)Y;
            }
        }

        public float Zf
        {
            get
            {
                return (float)Z;
            }
        }

        public Regex NameRegex
        {
            get
            {
                return _nameRegex;
            }
        }
        #endregion Base Properties

        #region Graphical Properties
        // Graphical Properties
        public Vector3 Xyz { get; set; }
        public Color DrawColor { get; set; }
        public float DrawSize { get; set; }
        public bool Is3D { get; set; }
        #endregion Graphical Properties

        #region Animation Properties
        // Animation Properties
        public bool IsHighlighted { get; set; }
        public bool IsAlerting { get; set; }
        public bool IsSelected { get; set; }
        public int CurrentStep { get; set; }
        public AnimationState AlertState { get; set; }
        public AnimationState HighlightState { get; set; }

        private int _stepAlert = 1;
        private int _stepHighlight;
        private int _alertPulseCount;

        // Derived Animation Properties
        public bool IsHighlightedAndAlerting
        {
            get
            {
                return (IsHighlighted && IsAlerting);
            }
        }

        public Color4 DrawColorC4
        {
            get
            {
                return new Color4(DrawColor.R, DrawColor.G, DrawColor.B, DrawColor.A);
            }
        }

        public int DrawColorArgb32
        {
            get
            {
                return Utility.ColorToRgba32(DrawColor);
            }
        }
        #endregion Animation Properties

        #region Methods
        public bool MatchNameRegex(string logLine)
        {
            if (_nameRegex.IsMatch(logLine))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ProcessTick()
        {
            //if (ProcessAlertTick() || ProcessHighlightTick())

            bool htr = ProcessHighlightTick();
            bool atr = ProcessAlertTick();

            if (htr || atr)
                return true;
            else
                return false;
        }

        public bool ProcessAlertTick()
        {
            int maxTick = 30;
            if ((_stepAlert < maxTick) && (_stepAlert > 0) && (AlertState != AnimationState.Idle))
            {
                if (AlertState == AnimationState.Growing)
                {
                    _stepAlert++;
                }
                else if (AlertState == AnimationState.Shrinking)
                {
                    _stepAlert--;
                }
            }

            if ((_stepAlert >= maxTick) && (AlertState == AnimationState.Growing))
            {
                AlertState = AnimationState.Shrinking;
                _stepAlert--;
            }
            else if((_stepAlert <= 0) && (AlertState == AnimationState.Shrinking))
            {
                AlertState = AnimationState.Growing;
                _stepAlert++;
                _alertPulseCount++;
            }

            if (_alertPulseCount > 4)
            {
                AlertState = AnimationState.Idle;
                IsAlerting = false;
                DrawColor = DefaultDrawColor;
                DrawSize = 0;
                _alertPulseCount = 0;
                return true;
            }
            else
            {
                //DrawSize = _stepAlert;

                if (AlertState == AnimationState.Growing)
                {
                    DrawSize = (float)PennerDoubleAnimation.QuintEaseIn(_stepAlert, 1, 100, maxTick);
                }
                else if (AlertState == AnimationState.Shrinking)
                {
                    DrawSize = (float)PennerDoubleAnimation.QuintEaseIn(_stepAlert, 1, 100, maxTick);
                }

                return false;
            }
        }

        public bool ProcessHighlightTick()
        {
            int maxTick = 20;
            if ((_stepHighlight < maxTick) && (_stepHighlight > 0) && (HighlightState != AnimationState.Idle))
            {
                if (HighlightState == AnimationState.Growing)
                {
                    _stepHighlight++;
                }
                else if (HighlightState == AnimationState.Shrinking)
                {
                    _stepHighlight--;
                }
            }

            if ((_stepHighlight >= maxTick) && (HighlightState == AnimationState.Growing))
            {
                HighlightState = !_isFlash ? AnimationState.Paused : AnimationState.Shrinking;
                _stepHighlight--;
            }
            else if ((_stepHighlight <= 0) && (HighlightState == AnimationState.Shrinking))
            {
                _stepHighlight++;
                HighlightState = AnimationState.Idle;
                IsHighlighted = false;
                _isFlash = false;
                DrawColor = DefaultDrawColor;
                DrawSize = 0;
                return true;
            }

            //DrawSize = _stepAlert;

            if (HighlightState == AnimationState.Growing)
            {
                DrawSize = (float)PennerDoubleAnimation.QuintEaseOut(_stepHighlight, 1, 10, maxTick);
            }
            else if (HighlightState == AnimationState.Shrinking)
            {
                DrawSize = (float)PennerDoubleAnimation.QuintEaseIn(_stepHighlight, 1, 10, maxTick);
            }

            return false;
        }

        public void ResetHighlight()
        {
            _stepHighlight++;
            HighlightState = AnimationState.Idle;
            IsHighlighted = false;
            DrawColor = DefaultDrawColor;
            DrawSize = 0;
        }

        public void ClearAlert()
        {
            DrawSize = 0.0f;

        }

        public void ClearHighlight()
        {
            DrawSize = 0.0f;
        }
        #endregion Methods

        #region Helpers
        public void StartAlert()
        {
            IsAlerting = true;
            AlertState = AnimationState.Growing;
            DrawColor = AlertingDrawColor;
            _stepAlert = 1;
        }

        private bool _isFlash;

        public void StartHighlight(bool flash = false)
        {
            IsHighlighted = true;
            HighlightState = AnimationState.Growing;
            DrawColor = HighlightDrawColor;
            _isFlash = flash;
            _stepHighlight = 1;
        }
        #endregion Helpers
    }

    #region Enums
    public enum AnimationState
    {
        Growing,
        Paused,
        Shrinking,
        Idle
    }
    #endregion Enums
}
