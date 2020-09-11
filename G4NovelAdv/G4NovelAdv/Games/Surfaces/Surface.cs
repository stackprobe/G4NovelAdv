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

		public string ClassName; // セーブ・ロード用
		public string InstanceName;
		public int X;
		public int Y;
		public int Z;

		// <---- prm

		/// <summary>
		/// 描画する。
		/// </summary>
		public abstract void Draw();

		public void Invoke(string command, params string[] arguments)
		{
			int c = 0;

			switch (command)
			{
				case "移動":
					switch (arguments.Length)
					{
						case 3:
							this.X = int.Parse(arguments[c++]);
							this.Y = int.Parse(arguments[c++]);
							this.Z = int.Parse(arguments[c++]);
							break;

						case 2:
							this.X = int.Parse(arguments[c++]);
							this.Y = int.Parse(arguments[c++]);
							break;

						default:
							throw new DDError();
					}
					break;

				case "Z":
					this.Z = int.Parse(arguments[c++]);
					break;

				default:
					this.Invoke2(command, arguments);
					break;
			}
		}

		public string Serialize()
		{
			return new AttachString().Untokenize(new string[]
			{
				this.ClassName,
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

			this.ClassName = lines[c++];
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
		protected abstract void Invoke2(string command, string[] arguments);

		/// <summary>
		/// シリアライザ
		/// 現在の状態を再現可能な文字列を返す。
		/// </summary>
		/// <returns></returns>
		protected abstract string Serialize2();

		/// <summary>
		/// シリアライザ実行時の状態を再現する。
		/// </summary>
		/// <param name="value">シリアライザから取得した文字列</param>
		protected abstract void Deserialize2(string value);
	}
}
