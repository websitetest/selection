using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SelectionSolution.InstallPlan
{
	public class SelectionArea
	{
		private Canvas _canvas;
		private double _offsetX;
		private double _offsetY;
		private double _areaWidth;
		private double _areaHeight;
		private double _rotation = 0;
		private Canvas _areaCanvas;
		private Rectangle _resizeLeftRect;
		private Rectangle _resizeTopRect;
		private Rectangle _resizeRightRect;
		private Rectangle _resizeBottomRect;
		private Ellipse _rotateEllipse;

		public enum ResizeSide
		{
			NONE, LEFT, TOP, RIGHT, BOTTOM
		}

		private const double ResizerWidth = 10;
		private const double ResizerHeight = 10;
		private const double RotatorWidth = 10;
		private const double RotatorHeight = 10;

		private RotateTransform _rotateTransform;

		public double OffsetX
		{
			get { return _offsetX; }
			set
			{
				_offsetX = value;
				UpdatePositions();
			}
		}

		public double OffsetY
		{
			get { return _offsetY; }
			set
			{
				_offsetY = value;
				UpdatePositions();
			}
		}

		public double AreaWidth
		{
			get { return _areaWidth; }
			set { _areaWidth = value; UpdateSize(); }
		}

		public double AreaHeight
		{
			get { return _areaHeight; }
			set { _areaHeight = value; UpdateSize(); }
		}

		public double Rotation
		{
			get { return _rotation; }
			set
			{
				_rotation = value;
				_rotateTransform.Angle = _rotation;
			}
		}

		public RotateTransform RotateTransform
		{
			get { return _rotateTransform; }
		}

		public Canvas AreaCanvas
		{
			get { return _areaCanvas; }
		}

		public Rectangle ResizeLeftRect
		{
			get { return _resizeLeftRect; }
		}

		public Rectangle ResizeTopRect
		{
			get { return _resizeTopRect; }
		}

		public Rectangle ResizeRightRect
		{
			get { return _resizeRightRect; }
		}

		public Rectangle ResizeBottomRect
		{
			get { return _resizeBottomRect; }
		}

		public Ellipse RotatorEllipse
		{
			get { return _rotateEllipse; }
		}

		public SelectionArea(Canvas canvas, double offsetX, double offsetY)
		{
			_canvas = canvas;
			_offsetX = offsetX;
			_offsetY = offsetY;

			_areaCanvas = new Canvas();
			_areaCanvas.Background = new SolidColorBrush(Color.FromArgb(100, 200, 200, 200));

			_resizeLeftRect = new Rectangle();
			_resizeLeftRect.Width = ResizerWidth;
			_resizeLeftRect.Height = ResizerHeight;
			_resizeLeftRect.Fill = new SolidColorBrush(Color.FromArgb(200, 200, 200, 200));
			_resizeLeftRect.Cursor = Cursors.SizeWE;

			_resizeTopRect = new Rectangle();
			_resizeTopRect.Width = ResizerWidth;
			_resizeTopRect.Height = ResizerHeight;
			_resizeTopRect.Fill = new SolidColorBrush(Color.FromArgb(200, 200, 200, 200));
			_resizeTopRect.Cursor = Cursors.SizeNS;

			_resizeRightRect = new Rectangle();
			_resizeRightRect.Width = ResizerWidth;
			_resizeRightRect.Height = ResizerHeight;
			_resizeRightRect.Fill = new SolidColorBrush(Color.FromArgb(200, 200, 200, 200));
			_resizeRightRect.Cursor = Cursors.SizeWE;

			_resizeBottomRect = new Rectangle();
			_resizeBottomRect.Width = ResizerWidth;
			_resizeBottomRect.Height = ResizerHeight;
			_resizeBottomRect.Fill = new SolidColorBrush(Color.FromArgb(200, 200, 200, 200));
			_resizeBottomRect.Cursor = Cursors.SizeNS;

			_rotateEllipse = new Ellipse();
			_rotateEllipse.Width = RotatorWidth;
			_rotateEllipse.Height = RotatorHeight;
			_rotateEllipse.Fill = new SolidColorBrush(Color.FromArgb(200, 250, 250, 200));

			_rotateTransform = new RotateTransform(_rotation);
			_areaCanvas.RenderTransformOrigin = new Point(0.5, 0.5);
			_areaCanvas.RenderTransform = _rotateTransform;
		}

		protected void UpdatePositions()
		{
			SetElementPosition(_areaCanvas, _offsetX, _offsetY);

			double resizerWidthHalf = ResizerWidth / 2;
			double resizerHeightHalf = ResizerHeight / 2;
			double rotatorWidthHalf = RotatorWidth / 2;
			double rotatorHeightHalf = RotatorHeight / 2;

			SetElementPosition(_resizeLeftRect, -resizerWidthHalf, (_areaHeight / 2) - resizerHeightHalf);
			SetElementPosition(_resizeTopRect, (_areaWidth / 2) - resizerWidthHalf, -resizerHeightHalf);
			SetElementPosition(_resizeRightRect, _areaWidth - resizerWidthHalf, (_areaHeight / 2) - resizerHeightHalf);
			SetElementPosition(_resizeBottomRect, (_areaWidth / 2) - resizerWidthHalf, _areaHeight - resizerHeightHalf);
			SetElementPosition(_rotateEllipse, (_areaWidth / 2) - rotatorWidthHalf, -12 - rotatorHeightHalf);
		}

		protected void UpdateSize()
		{
			_areaCanvas.Width = _areaWidth;
			_areaCanvas.Height = _areaHeight;

			UpdatePositions();
		}

		private void SetElementPosition(UIElement element, double x, double y)
		{
			Canvas.SetLeft(element, x);
			Canvas.SetTop(element, y);
		}

		public Point CalculateCenterPoint()
		{
			return new Point(_offsetX + (_areaWidth / 2), _offsetY + (_areaHeight / 2));
		}

		public void AddToCanvas(Canvas canvas)
		{
			UpdatePositions();
			canvas.Children.Add(_areaCanvas);

			_areaCanvas.Children.Add(_resizeLeftRect);
			_areaCanvas.Children.Add(_resizeTopRect);
			_areaCanvas.Children.Add(_resizeRightRect);
			_areaCanvas.Children.Add(_resizeBottomRect);
			_areaCanvas.Children.Add(_rotateEllipse);
		}
	}
}