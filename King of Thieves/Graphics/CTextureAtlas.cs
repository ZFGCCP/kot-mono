using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;

namespace King_of_Thieves.Graphics
{
    public class CTextureAtlas : IDisposable
    {
        public int FrameWidth = 0, FrameHeight = 0, FrameRate = 0, CellSpacing = 0, Column = 0, Row = 0, CurrentCell = 0;
        private Rectangle[,] _textureAtlas;
        private string _atlasName;
        private Texture2D _sourceImage;
        private int _fixedWidth = 0, _fixedHeight = 0;
        private static Regex _cellFormat = new Regex("^[0-9]+:[0-9]+$");
        private static Regex _cellSplitter = new Regex(":");
        private bool _isTileSet;

        public CTextureAtlas(Texture2D sourceImage, string source, int _frameWidth, int _frameHeight, int _cellSpacing)
        {
            FrameWidth = _frameWidth;
            FrameHeight = _frameHeight;
            CellSpacing = _cellSpacing;
            _sourceImage = sourceImage;

            _fixedWidth = (_sourceImage.Bounds.Width / (_frameWidth + _cellSpacing));
            _fixedHeight = (_sourceImage.Bounds.Height / (_frameHeight + _cellSpacing));

            _textureAtlas = new Rectangle[_fixedWidth, _fixedHeight];//made a small change here to allow for cellspacing in the calculation. -Steve
            _assembleTextureAtlas(this);
        }

        public CTextureAtlas(Texture2D sourceImage, int _frameWidth, int _frameHeight, int _cellSpacing, int frameRate)
        {
            _setup(ref sourceImage, _frameWidth, _frameHeight, _cellSpacing, frameRate);
        }

        public CTextureAtlas(string sourceImage, int _frameWidth, int _frameHeight, int _cellSpacing, string startCell, string endCell, int frameRate = 0, bool flipH = false, bool flipV = false, bool isTileSet = false, bool reverse = false)
        {
            //parse out the cell ranges
            if (!_cellFormat.IsMatch(startCell) || !_cellFormat.IsMatch(endCell))
                throw new FormatException("Error in cell range format for " + sourceImage + ". Please use 99:99");
            
            string[] start = _cellSplitter.Split(startCell);
            string[] end = _cellSplitter.Split(endCell);
            Vector2 _startCell = new Vector2(Convert.ToInt32(start[0]), Convert.ToInt32(start[1]));
            Vector2 _endCell = new Vector2(Convert.ToInt32(end[0]), Convert.ToInt32(end[1]));
            float cellsX = _endCell.X - _startCell.X;
            float cellsY = _endCell.Y - _startCell.Y;
            int flipXOffSet = flipH ? 1 : 0;
            int flipYOffSet = flipV ? 1 : 0;
            _atlasName = sourceImage;
            _isTileSet = isTileSet;



            Rectangle fullRange = new Rectangle((int)((_startCell.X * _frameWidth +  (_cellSpacing * _startCell.X)) - flipXOffSet),
                                                (int)((_startCell.Y * _frameHeight + (_cellSpacing * _startCell.Y)) - flipYOffSet),
                                                (int)((_frameWidth + _cellSpacing) * (cellsX + 1)), 
                                                (int)((_frameHeight + _cellSpacing) * (cellsY + 1)));

            if (_startCell.X == 0)
                fullRange.X = 0;
            if (_startCell.Y == 0)
                fullRange.Y = 0;

            _setup(sourceImage, fullRange, _frameWidth, _frameHeight, _cellSpacing, frameRate, reverse);
        }

        private void _setup(string sourceImage, Rectangle sourceRect, int _frameWidth, int _frameHeight, int _cellSpacing, int frameRate, bool reverse = false)
        {
            FrameWidth = _frameWidth;
            FrameHeight = _frameHeight;
            CellSpacing = _cellSpacing;
            _sourceImage = CTextures.rawTextures[sourceImage];
            FrameRate = frameRate;

            _fixedWidth = (sourceRect.Width / (_frameWidth + _cellSpacing));
            _fixedHeight = (sourceRect.Height / (_frameHeight + _cellSpacing));
            _textureAtlas = new Rectangle[_fixedWidth, _fixedHeight];
            _assembleTextureAtlas(this, sourceRect.X / (_frameWidth + CellSpacing), sourceRect.Y / (_frameHeight + CellSpacing), reverse);

        }

        private void _setup(ref Texture2D sourceImage, int _frameWidth, int _frameHeight, int _cellSpacing, int frameRate)
        {
            FrameWidth = _frameWidth;
            FrameHeight = _frameHeight;
            CellSpacing = _cellSpacing;
            _sourceImage = sourceImage;
            FrameRate = frameRate;

            _fixedWidth = (_sourceImage.Bounds.Width / (_frameWidth + _cellSpacing));
            _fixedHeight = (_sourceImage.Bounds.Height / (_frameHeight + _cellSpacing));

            _textureAtlas = new Rectangle[_fixedWidth, _fixedHeight];
            _assembleTextureAtlas(this);
        }

        public bool isTileSet
        {
            get
            {
                return _isTileSet;
            }
        }

        public Texture2D sourceImage
        {
            get
            {
                return _sourceImage;
            }
        }

        public string source
        {
            get
            {
                return _atlasName;
            }
        }

        public int tileXCount
        {
            get
            {
                return _fixedWidth;
            }
        }

        public int tileYCount
        {
            get
            {
                return _fixedHeight;
            }
        }

        public int frameWidth
        {
            get
            {
                return _fixedWidth;
            }
        }

        public int frameHeight
        {
            get
            {
                return _fixedHeight;
            }
        }

        public Rectangle getTile(int frameX, int frameY)
        {
            return this._textureAtlas[frameX, frameY];
        }


        public void Save(MemoryStream memStream)
        {
            byte blue;
            Rectangle rect = new Rectangle(0, 0, _sourceImage.Width, _sourceImage.Height);
            byte[] textureData = new byte[4 * _sourceImage.Width * _sourceImage.Height];

            _sourceImage.GetData<byte>(textureData);
            for (int i = 0; i < textureData.Length; i += 4)
            {
                blue = textureData[i];
                textureData[i] = textureData[i + 2];
                textureData[i + 2] = blue;
            }
            memStream.Write(textureData, 0, textureData.Length);
            memStream.Seek(0, SeekOrigin.Begin);
        }

        /*
            * x == row
            * y == column
        */
        private void _assembleTextureAtlas(CTextureAtlas textureAtlas, int xStart = 0, int yStart = 0, bool reverse = false)
        {
            int xStartHold = xStart;
            int yStartHold = yStart;

            for (int y = 0; y <= _fixedHeight - 1; y++, yStart++)
            {
                xStart = xStartHold;
                for (int x = 0; x <= _fixedWidth - 1; x++, xStart++)
                {
                    //NOW the math is completely fine! :)
                    //The math is completely fine. 
                    //this math seems a bit iffy due to the cellspacing, but we'll see how it goes! -Steve
                    int x0 = 0;
                    int y0 = 0;

                    if (reverse)
                    {
                        x0 = _fixedWidth - 1 - x;
                        y0 = _fixedHeight - 1 - y;
                    }
                    else
                    {
                        x0 = x;
                        y0 = y;
                    }

                    textureAtlas._textureAtlas[x0,y0] = new Rectangle
                        ((textureAtlas.FrameWidth + CellSpacing) * xStart, (textureAtlas.FrameHeight + CellSpacing) * yStart,
                        textureAtlas.FrameWidth, 
                        textureAtlas.FrameHeight);
                }
            }
        }

        public new void Dispose()
        {
            _textureAtlas = null;
            _sourceImage.Dispose();
            _sourceImage = null;
        }
    }
}
