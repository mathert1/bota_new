using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WpfTest.Game
{
	public class Camera
	{
		public Matrix _transform;
		public float Zoom;
		public Vector2 Pos;
		public Vector2 scrolledPos;
		int screenWidth;
		int screenHeight;
		int mapWidth;
		int mapHeight;

		public Camera(int sW, int sH, int mW, int mH)
		{
			Zoom = 1.0f;
			Pos = new Vector2(0, 0);
			screenWidth = sW;
			screenHeight = sH;
			mapWidth = mW;
			mapHeight = mH;
		}

		public Matrix get_transformation()
		{
			_transform =
			  Matrix.CreateTranslation(new Vector3(-Pos.X, -Pos.Y, 0)) *
										 Matrix.CreateScale(new Vector3(Zoom, Zoom, 0)) *
										 Matrix.CreateTranslation(new Vector3(0, 0, 0));
			return _transform;
		}

		public void ScrollCamera(Vector2 pos)
		{
			const float ViewMargin = .5f;
			scrolledPos = pos;
			float marginWidth = screenWidth / Zoom * ViewMargin;
			float marginHeight = screenHeight / Zoom * ViewMargin;
			float marginLeft = Pos.X + marginWidth;
			float marginRight = Pos.X - marginWidth + screenWidth / Zoom;
			float marginTop = Pos.Y + marginHeight;
			float marginBottom = Pos.Y - marginHeight + screenHeight / Zoom;

			float cameraMovementX = 0.0f;
			if (pos.X < marginLeft)
				cameraMovementX = pos.X - marginLeft;
			else if (pos.X > marginRight)
				cameraMovementX = pos.X - marginRight;

			float cameraMovementY = 0.0f;
			if (pos.Y < marginTop)
				cameraMovementY = pos.Y - marginTop;
			else if (pos.Y > marginBottom)
				cameraMovementY = pos.Y - marginBottom;

			float maxCameraPositionX = mapWidth - screenWidth / Zoom;
			float maxCameraPositionY = mapHeight - screenHeight / Zoom;
			//cam.Pos = new Vector2(MathHelper.Clamp(cam.Pos.X + cameraMovementX, 0.0f, maxCameraPositionX),
			//    MathHelper.Clamp(cam.Pos.Y + cameraMovementY, 0.0f, maxCameraPositionY));
			Pos = new Vector2(MathHelper.Clamp(Pos.X + cameraMovementX, 0.0f, maxCameraPositionX),
				MathHelper.Clamp(Pos.Y + cameraMovementY, -300.0f, maxCameraPositionY));
		}
	}
}
