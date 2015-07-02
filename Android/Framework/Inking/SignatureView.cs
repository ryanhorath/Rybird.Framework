using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;
using Android.Util;

namespace Rybird.Framework
{
    public class SignatureView : View
    {
        private Bitmap _bitmap;
        private Canvas _canvas;
        private Path _path;
        private Paint _bitmapPaint;
        private Paint _paint;
        private SignatureData _signatureData;
        public SignatureData SignatureData
        {
            get { return _signatureData; }
            private set { _signatureData = value; }
        }

        public SignatureView(Context context, IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle)
        {
            Initialize();
        }

        public SignatureView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Initialize();
        }

        public SignatureView(Context context)
            : base(context)
        {
            Initialize();
        }

        private void Initialize()
        {
            _path = new Path();
            _bitmapPaint = new Paint(PaintFlags.Dither);
            _paint = new Paint
            {
                AntiAlias = true,
                Dither = true,
                Color = Color.Argb(255, 0, 0, 0)
            };
            _paint.SetStyle(Paint.Style.Stroke);
            _paint.StrokeJoin = Paint.Join.Round;
            _paint.StrokeCap = Paint.Cap.Round;
            _paint.StrokeWidth = 8;
            SignatureData = new SignatureData();
        }

        protected override void OnSizeChanged(int w, int h, int oldw, int oldh)
        {
            base.OnSizeChanged(w, h, oldw, oldh);
            _bitmap = Bitmap.CreateBitmap(w, (h > 0 ? h : ((View)this.Parent).Height), Bitmap.Config.Argb8888);
            //_bitmap = BitmapFactory.DecodeResource(Resources, Resource.Drawable.SignHere);
            _canvas = new Canvas(_bitmap);
        }

        protected override void OnDraw(Canvas canvas)
        {
            canvas.DrawBitmap(_bitmap, 0, 0, _bitmapPaint);
            canvas.DrawPath(_path, _paint);
        }

        private float _mX, _mY;
        private const float TouchTolerance = 4;

        private void TouchStart(float x, float y)
        {
            _path.Reset();
            _path.MoveTo(x, y);
            _mX = x;
            _mY = y;
            SignatureData.AddPoint(SignatureState.Start, (int)x, (int)y);
        }

        private void TouchMove(float x, float y)
        {
            float dx = Math.Abs(x - _mX);
            float dy = Math.Abs(y - _mY);

            if (dx >= TouchTolerance || dy >= TouchTolerance)
            {
                _path.QuadTo(_mX, _mY, (x + _mX) / 2, (y + _mY) / 2);
                SignatureData.AddPoint(SignatureState.Move, (int)x, (int)y);
                _mX = x;
                _mY = y;
            }
        }

        private void TouchUp()
        {
            if (!_path.IsEmpty)
            {
                _path.LineTo(_mX, _mY);
                _canvas.DrawPath(_path, _paint);
            }
            else
            {
                _canvas.DrawPoint(_mX, _mY, _paint);
            }
            SignatureData.AddPoint(SignatureState.End, (int)_mX, (int)_mY);

            _path.Reset();
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            var x = e.GetX();
            var y = e.GetY();

            switch (e.Action)
            {
                case MotionEventActions.Down:
                    TouchStart(x, y);
                    Invalidate();
                    break;
                case MotionEventActions.Move:
                    TouchMove(x, y);
                    Invalidate();
                    break;
                case MotionEventActions.Up:
                    TouchUp();
                    Invalidate();
                    break;
            }
            return true;
        }

        public void ClearCanvas()
        {
            _canvas.DrawColor(Color.White);
            Invalidate();
        }

        public Bitmap CanvasBitmap()
        {
            return _bitmap;
        }

        public void Clear()
        {
            ClearCanvas();
            SignatureData = new SignatureData();
        }
    }
}