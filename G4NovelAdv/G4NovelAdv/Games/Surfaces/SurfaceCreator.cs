﻿using System;
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
				case "からい": surface = new Surface_からい(); break;
				case "結月ゆかり": surface = new Surface_結月ゆかり(); break;
				case "弦巻マキ": surface = new Surface_弦巻マキ(); break;
				case "東北ずん子": surface = new Surface_東北ずん子(); break;

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
