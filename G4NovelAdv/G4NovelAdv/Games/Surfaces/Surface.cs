using System;
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
		/// <para>空のとき無効</para>
		/// <para>空ではないとき Draw の代わりに最初の要素を実行する。</para>
		/// <para>ret: ? このアクションを継続する。</para>
		/// <para>セーブするとき、このフィールドは保存しない。</para>
		/// </summary>
		public List<Func<bool>> Acts = null;

		public string TypeName;
		public string InstanceName;

		// <---- prm

		public int X = DEFAULT_X;
		public int Y = DEFAULT_Y;
		public int Z = DEFAULT_Z;

		public const int DEFAULT_X = DDConsts.Screen_W / 2;
		public const int DEFAULT_Y = DDConsts.Screen_H / 2;
		public const int DEFAULT_Z = 0;

		/// <summary>
		/// 描画する。
		/// </summary>
		public abstract void Draw();

		public void Invoke(string command, params string[] arguments)
		{
			int c = 0;

			if (command == "移動")
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
			else if (command == "X")
			{
				this.X = int.Parse(arguments[c++]);
			}
			else if (command == "Y")
			{
				this.Y = int.Parse(arguments[c++]);
			}
			else if (command == "Z")
			{
				this.Z = int.Parse(arguments[c++]);
			}
			else
			{
				this.Invoke2(command, arguments);
			}
		}

		public string Serialize()
		{
			return new AttachString().Untokenize(new string[]
			{
				this.TypeName,
				this.InstanceName,
				this.X.ToString(),
				this.Y.ToString(),
				this.Z.ToString(),
				this.Serialize2(),
			});
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
			this.Deserialize2(lines[c++]);
		}

		/// <summary>
		/// 固有のコマンドを実行する。
		/// </summary>
		/// <param name="command">コマンド名</param>
		/// <param name="arguments">コマンド引数列</param>
		protected virtual void Invoke2(string command, string[] arguments)
		{
			throw new DDError();
		}

		/// <summary>
		/// シリアライザ
		/// 現在の状態を再現可能な文字列を返す。
		/// </summary>
		/// <returns></returns>
		protected virtual string Serialize2()
		{
			return "";
		}

		/// <summary>
		/// シリアライザ実行時の状態を再現する。
		/// </summary>
		/// <param name="value">シリアライザから取得した文字列</param>
		protected virtual void Deserialize2(string value)
		{
			if (value != "")
				throw new DDError();
		}
	}
}
