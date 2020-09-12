using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;

namespace Charlotte.Games.Surfaces
{
	public static class SurfaceCreator
	{
		public static Surface Create(string typeName, string instanceName)
		{
			Surface surface;

			// HACK: typeName はクラス名 Surface_<typeName> と対応する。リフレクションでも良い。-> 難読化するので不可

			switch (typeName)
			{
				case "Dark": surface = new Surface_Dark(); break;
				case "Smpl0001": surface = new Surface_Smpl0001(); break;
				case "UFOYukari": surface = new Surface_UFOYukari(); break;

				// 新しいキャラクタをここへ追加..

				default:
					throw new DDError("不明なタイプ名：" + typeName);
			}
			surface.TypeName = typeName;
			surface.InstanceName = instanceName;

			return surface;
		}
	}
}
