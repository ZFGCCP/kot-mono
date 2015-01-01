using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Gears.Navigation
{
    [XmlRootAttribute("MenuElement", Namespace = "http://www.spectrumbranch.com", IsNullable = false)]
    public class MenuElement
    {
        [XmlAttribute("Hidden")]
        public bool Hidden = false;
        
        /// <summary>
        /// ThrowPushAction is not serializable, because System.Action is not serializable.
        /// All ThrowPushActions must be manually assigned.
        /// </summary>
        [XmlIgnore]
        private Action _throwPushAction;

        [XmlIgnore]
        private SpriteFont _font = null;

        [XmlIgnore]
        private Texture2D _texture;

        [XmlElement("ActiveArea", IsNullable = false)]
        public Rectangle ActiveArea;

        [XmlElement("ForegroundColor", IsNullable = false)]
        public Color ForegroundColor;

        [XmlElement("ActiveForegroundColor", IsNullable = false)]
        public Color ActiveForegroundColor;

        [XmlElement("BackgroundColor", IsNullable = false)]
        public Color BackgroundColor;

        [XmlElement("ActiveBackgroundColor", IsNullable = false)]
        public Color ActiveBackgroundColor;

        [XmlElement("Selectable", IsNullable = false)]
        public bool Selectable = false;

        [XmlElement("MenuText", IsNullable = false)]
        public String MenuText = "";

        /// <summary>
        /// The SpriteFont field is used only during the loading phase, if this._font is not already set.
        /// Set this field equal to the content-friendly filepath of a valid SpriteFont file.
        /// </summary>
        [XmlElement("SpriteFont", IsNullable = true)]
        public String SpriteFont = null;

        /// <summary>
        /// The Texture2D field is used only during the loading phase, if this._texture is not already set.
        /// Set this field equal to the content-friendly filepath of a valid Texture2D file.
        /// </summary>
        [XmlElement("Texture2D", IsNullable = true)]
        public String Texture2D = null;

        
        public MenuElement() { }

        public void SetThrowPushEvent(Action action)
        {
            if (action != null)
            {
                this._throwPushAction = action;
            }
        }
        public virtual void ThrowPushEvent()
        {
            if (this._throwPushAction != null)
            {
                this._throwPushAction();
            }
        }
        public void SetFont(SpriteFont font)
        {
            if (font != null)
            {
                this._font = font;
            }
        }
        public SpriteFont GetFont()
        {
            return this._font;
        }
        public void SetTexture(Texture2D texture)
        {
            if (texture != null)
            {
                this._texture = texture;
            }
        }
        public Texture2D GetTexture()
        {
            return this._texture;
        }
    }
}
