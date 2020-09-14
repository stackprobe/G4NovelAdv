﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;
using Charlotte.Tools;

namespace Charlotte.Games.Surfaces
{
	/// <summary>
	/// <para>現在登場中のキャラクタやオブジェクトの状態を保持する。</para>
	/// <para>GameStatus の一部であるため、セーブ・ロード時にこのクラスの内容を保存・再現する。</para>
	/// </summary>
	public abstract class Surface
	{
		/// <summary>
		/// <para>アクションのリスト</para>
		/// <para>Act.Draw が false を返したとき this.Draw を実行しなければならない。</para>
		/// <para>セーブするとき、このフィールドは保存しない。</para>
		/// <para>アクションは途中で破棄されても良いように実装すること。</para>
		/// <para>- スキップモード等で Act.Clear が実行されるかもしれない。</para>
		/// </summary>
		public Act Act = new Act();

		public string TypeName; // セーブ・ロード用
		public string InstanceName;

		// <---- prm

		public int X = DEFAULT_X;
		public int Y = DEFAULT_Y;
		public int Z = DEFAULT_Z;

		public const int DEFAULT_X = DDConsts.Screen_W / 2;
		public const int DEFAULT_Y = DDConsts.Screen_H / 2;
		public const int DEFAULT_Z = 0;

		/// <summary>
		/// 自分自身を削除する。
		/// </summary>
		public void RemoveMe()
		{
			Game.I.Status.RemoveSurface(this);
		}

		/// <summary>
		/// <para>コマンドを実行する。</para>
		/// <para>ここでは共通のコマンドを処理し、それ以外のコマンドを処理するために Invoke_02 を呼び出す。</para>
		/// </summary>
		/// <param name="command">コマンド名</param>
		/// <param name="arguments">コマンド引数列</param>
		public void Invoke(string command, params string[] arguments)
		{
			int c = 0;

			if (command == ScenarioWords.COMMAND_移動)
			{
				if (arguments.Length == 3)
				{
					this.X = int.Parse(arguments[c++]);
					this.Y = int.Parse(arguments[c++]);
					this.Z = int.Parse(arguments[c++]);
				}
				else if (arguments.Length == 2)
				{
					this.X = int.Parse(arguments[c++]);
					this.Y = int.Parse(arguments[c++]);
				}
				else
				{
					throw new DDError();
				}
			}
			else if (command == ScenarioWords.COMMAND_X)
			{
				this.X = int.Parse(arguments[c++]);
			}
			else if (command == ScenarioWords.COMMAND_Y)
			{
				this.Y = int.Parse(arguments[c++]);
			}
			else if (command == ScenarioWords.COMMAND_Z)
			{
				this.Z = int.Parse(arguments[c++]);
			}
			else if (command == ScenarioWords.COMMAND_End)
			{
				this.RemoveMe();
			}
			else
			{
				this.Invoke_02(command, arguments);
			}
		}

		/// <summary>
		/// シリアライザ
		/// 現在の「固有の状態」を再現可能な文字列を返す。
		/// </summary>
		/// <returns></returns>
		public string Serialize()
		{
			return new AttachString().Untokenize(EnumerableTools.Linearize(new string[][]
			{
				new string[]
				{
					this.TypeName,
					this.InstanceName,
					this.X.ToString(),
					this.Y.ToString(),
					this.Z.ToString(),
				},
				this.Serialize_02(),
			}
			));
		}

		public void Deserialize(string value)
		{
			string[] lines = new AttachString().Tokenize(value);
			int c = 0;

			this.TypeName = lines[c++];
			this.InstanceName = lines[c++];
			this.X = int.Parse(lines[c++]);
			this.Y = int.Parse(lines[c++]);
			this.Z = int.Parse(lines[c++]);
			this.Deserialize_02(lines.Skip(c).ToArray());
		}

		/// <summary>
		/// 描画する。
		/// </summary>
		public abstract void Draw();

		/// <summary>
		/// 固有のコマンドを実行する。
		/// </summary>
		/// <param name="command">コマンド名</param>
		/// <param name="arguments">コマンド引数列</param>
		protected virtual void Invoke_02(string command, params string[] arguments)
		{
			throw new DDError();
		}

		private static readonly string[] SERIALIZED_DUMMY = new string[] { "SERIALIZED_DUMMY" };

		/// <summary>
		/// シリアライザ
		/// 現在の「固有の状態」を再現可能な文字列の配列を返す。
		/// </summary>
		/// <returns>状態データ</returns>
		protected virtual string[] Serialize_02()
		{
			return SERIALIZED_DUMMY;
		}

		/// <summary>
		/// シリアライザ実行時の「固有の状態」を再現する。
		/// </summary>
		/// <param name="lines">シリアライザから取得した状態データ</param>
		protected virtual void Deserialize_02(string[] lines)
		{
			if (ArrayTools.Comp(lines, SERIALIZED_DUMMY, StringTools.Comp) != 0)
				throw new DDError();
		}
	}
}
