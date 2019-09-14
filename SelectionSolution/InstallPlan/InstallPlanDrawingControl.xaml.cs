using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SelectionSolution.InstallPlan
{
	/// <summary>
	/// Interaction logic for InstallPlanDrawingControl.xaml
	/// InstallPlanDrawingControl is a UserControl
	/// </summary>
	public partial class InstallPlanDrawingControl : UserControl, INotifyPropertyChanged
	{
		private BitmapImage _image;

		private bool _isMouseDown = false;
		private Point _mouseDownPoint;

		private List<SelectionArea> _areas = new List<SelectionArea>();
		private SelectionArea _activeInsertArea = null;
		private SelectionArea _activeResizeArea = null;
		private double _activeResizeAreaXbeforeStart;
		private double _activeResizeAreaYbeforeStart;
		private double _activeResizeAreaWidthbeforeStart;
		private double _activeResizeAreaHeightbeforeStart;
		private SelectionArea.ResizeSide _activeResizeAreaSide = SelectionArea.ResizeSide.NONE;
		private SelectionArea _activeRotateArea = null;
		private double _activeRotateAreaRotationBeforeStart;
		private Point _activeRotateAreaCenterPointBeforeStart;

		/// <summary>
		/// Describes what type of edit the user id doing. Default is INSERTING. When the mode
		/// is different (like now RESIZING), the default INSERTING will be set when the RESIZING mode is done.
		/// </summary>
		protected enum EditMode
		{
			INSERTING, RESIZING, ROTATING, SKEWING
		}

		private EditMode _currentEditMode = EditMode.INSERTING;

		public BitmapImage Image
		{
			get { return (BitmapImage)GetValue(ImageProperty); }
			set { SetValue(ImageProperty, value); }
		}

		// DependencyProperty
		public static readonly DependencyProperty ImageProperty =
			DependencyProperty.Register("Image", typeof(BitmapImage), typeof(InstallPlanDrawingControl), new PropertyMetadata(null, ImageChanged));

		private static void ImageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			// Testing:
			//ImageViewWindow win = new ImageViewWindow();
			//win.SetBitmapImage(e.NewValue as BitmapImage);
			//win.Show();
		}

		public InstallPlanDrawingControl()
		{
			InitializeComponent();
			this.DataContext = this;

			drawCanvas.Cursor = Cursors.Pen;
		}

		private void drawCanvas_MouseDown(object sender, MouseButtonEventArgs e)
		{
			_isMouseDown = true;
			_mouseDownPoint = e.GetPosition(drawCanvas);

			if (_currentEditMode == EditMode.INSERTING)
			{

				SelectionArea area = new SelectionArea(drawCanvas, _mouseDownPoint.X, _mouseDownPoint.Y);
				area.AddToCanvas(drawCanvas);
				_areas.Add(area);

				_activeInsertArea = area;

				AttachMouseEvents(area);
			}
		}

		private void drawCanvas_MouseUp(object sender, MouseButtonEventArgs e)
		{
			_isMouseDown = false;
			drawCanvas.Cursor = Cursors.Pen;

			if (_currentEditMode == EditMode.INSERTING)
			{
				_activeInsertArea = null;
			}
			else if (_currentEditMode == EditMode.RESIZING)
			{
				_activeResizeArea = null;
				_activeResizeAreaSide = SelectionArea.ResizeSide.NONE;
				_currentEditMode = EditMode.INSERTING;
			}
			else if (_currentEditMode == EditMode.ROTATING)
			{
				_activeRotateArea = null;
				_currentEditMode = EditMode.INSERTING;
			}
		}

		private void drawCanvas_MouseMove(object sender, MouseEventArgs e)
		{
			if (_currentEditMode == EditMode.INSERTING)
			{
				if (_isMouseDown)
				{
					if (_activeInsertArea != null)
					{
						Point currentMousePoint = e.GetPosition(drawCanvas);

						_activeInsertArea.AreaWidth = Math.Abs(_mouseDownPoint.X - currentMousePoint.X);
						_activeInsertArea.AreaHeight = Math.Abs(_mouseDownPoint.Y - currentMousePoint.Y);

						if (currentMousePoint.X < _mouseDownPoint.X)
						{
							_activeInsertArea.OffsetX = currentMousePoint.X;
						}
						if (currentMousePoint.Y < _mouseDownPoint.Y)
						{
							_activeInsertArea.OffsetY = currentMousePoint.Y;
						}
					}
				}
			}
			else if (_currentEditMode == EditMode.RESIZING)
			{
				if (_isMouseDown)
				{
					if (_activeResizeArea != null)
					{
						UpdateResizeArea(e.GetPosition(drawCanvas));
					}
				}
			}
			else if (_currentEditMode == EditMode.ROTATING)
			{
				if (_isMouseDown)
				{
					if (_activeRotateArea != null)
					{
						UpdateRotateArea(e.GetPosition(drawCanvas));
					}
				}
			}
		}

		private void AttachMouseEvents(SelectionArea area)
		{
			area.ResizeLeftRect.MouseDown += (object sender, MouseButtonEventArgs e) => {
				StartResizing(area, SelectionArea.ResizeSide.LEFT);
			};
			area.ResizeTopRect.MouseDown += (object sender, MouseButtonEventArgs e) => {
				StartResizing(area, SelectionArea.ResizeSide.TOP);
			};
			area.ResizeRightRect.MouseDown += (object sender, MouseButtonEventArgs e) => {
				StartResizing(area, SelectionArea.ResizeSide.RIGHT);
			};
			area.ResizeBottomRect.MouseDown += (object sender, MouseButtonEventArgs e) => {
				StartResizing(area, SelectionArea.ResizeSide.BOTTOM);
			};

			area.RotatorEllipse.MouseDown += (object sender, MouseButtonEventArgs e) => {
				StartRotating(area);
			};
		}

		private void StartResizing(SelectionArea area, SelectionArea.ResizeSide side)
		{
			_currentEditMode = EditMode.RESIZING;

			_activeResizeArea = area;
			_activeResizeAreaSide = side;
			_activeResizeAreaXbeforeStart = area.OffsetX;
			_activeResizeAreaYbeforeStart = area.OffsetY;
			_activeResizeAreaWidthbeforeStart = area.AreaWidth;
			_activeResizeAreaHeightbeforeStart = area.AreaHeight;

			switch (side)
			{
				case SelectionArea.ResizeSide.LEFT:
					drawCanvas.Cursor = Cursors.SizeWE;
					break;
				case SelectionArea.ResizeSide.TOP:
					drawCanvas.Cursor = Cursors.SizeNS;
					break;
				case SelectionArea.ResizeSide.RIGHT:
					drawCanvas.Cursor = Cursors.SizeWE;
					break;
				case SelectionArea.ResizeSide.BOTTOM:
					drawCanvas.Cursor = Cursors.SizeNS;
					break;
			}
		}

		public void StartRotating(SelectionArea area)
		{
			_currentEditMode = EditMode.ROTATING;
			_activeRotateArea = area;
			_activeRotateAreaRotationBeforeStart = area.Rotation;
			_activeRotateAreaCenterPointBeforeStart = area.CalculateCenterPoint();
		}

		private void UpdateResizeArea(Point currentMousePoint)
		{
			Point currentMousePointTransformed = _activeResizeArea.AreaCanvas.PointFromScreen(currentMousePoint);
			Point mouseDownPointTransformed = _activeResizeArea.AreaCanvas.PointFromScreen(_mouseDownPoint);

			double deltaX = currentMousePointTransformed.X - mouseDownPointTransformed.X;
			double deltaY = currentMousePointTransformed.Y - mouseDownPointTransformed.Y;

			switch (_activeResizeAreaSide)
			{
				case SelectionArea.ResizeSide.LEFT:
					_activeResizeArea.OffsetX = _activeResizeAreaXbeforeStart + deltaX;
					_activeResizeArea.AreaWidth = _activeResizeAreaWidthbeforeStart - deltaX;
					break;
				case SelectionArea.ResizeSide.TOP:
					_activeResizeArea.OffsetY = _activeResizeAreaYbeforeStart + deltaY;
					_activeResizeArea.AreaHeight = _activeResizeAreaHeightbeforeStart - deltaY;
					break;
				case SelectionArea.ResizeSide.RIGHT:
					_activeResizeArea.AreaWidth = _activeResizeAreaWidthbeforeStart + deltaX;
					break;
				case SelectionArea.ResizeSide.BOTTOM:
					_activeResizeArea.AreaHeight = _activeResizeAreaHeightbeforeStart + deltaY;
					break;
			}

			if (_activeResizeArea.OffsetX > _activeResizeAreaXbeforeStart + _activeResizeAreaWidthbeforeStart)
			{
				_activeResizeAreaSide = SelectionArea.ResizeSide.RIGHT;
			}
			if (_activeResizeArea.OffsetY > _activeResizeAreaYbeforeStart + _activeResizeAreaHeightbeforeStart)
			{
				_activeResizeAreaSide = SelectionArea.ResizeSide.BOTTOM;
			}
			if (_activeResizeArea.AreaWidth < 0)
			{
				_activeResizeAreaSide = SelectionArea.ResizeSide.LEFT;
			}
			if (_activeResizeArea.AreaHeight < 0)
			{
				_activeResizeAreaSide = SelectionArea.ResizeSide.TOP;
			}
		}

		private void UpdateRotateArea(Point currentMousePoint)
		{
			// Calculate an angle
			double radians = Math.Atan((currentMousePoint.Y - _activeRotateAreaCenterPointBeforeStart.Y) /
									   (currentMousePoint.X - _activeRotateAreaCenterPointBeforeStart.X));
			double angle = radians * 180 / Math.PI;

			// Apply a 180 degree shift when X is negative so that we can rotate
			// all of the way around
			if (currentMousePoint.X - _activeRotateAreaCenterPointBeforeStart.X < 0)
			{
				angle += 180;
			}

			_activeRotateArea.Rotation = _activeRotateAreaRotationBeforeStart + angle;
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged(string name)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}

	}
}