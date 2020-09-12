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
		/// <para>Act.Draw が false を返したとき this.Draw を実行しなければならない。</para>
		/// <para>セーブするとき、このフィールドは保存しない。</para>
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
		/// 退場する。
		/// </summary>
		public void RemoveMe()
		{
			Game.I.Status.RemoveSurface(this);
		}

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
				Game.I.Status.RemoveSurface(this);
			}
			else
			{
				this.Invoke_02(command, arguments);
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
				this.Serialize_02(),
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
			this.Deserialize_02(lines[c++]);
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
		protected virtual void Invoke_02(string command, string[] arguments)
		{
			throw new DDError();
		}

		private const string SERIALIZED_DUMMY = "SERIALIZED_DUMMY";

		/// <summary>
		/// シリアライザ
		/// 現在の「固有の状態」を再現可能な文字列を返す。
		/// </summary>
		/// <returns></returns>
		protected virtual string Serialize_02()
		{
			return SERIALIZED_DUMMY;
		}

		/// <summary>
		/// シリアライザ実行時の「固有の状態」を再現する。
		/// </summary>
		/// <param name="value">シリアライザから取得した文字列</param>
		protected virtual void Deserialize_02(string value)
		{
			if (value != SERIALIZED_DUMMY)
				throw new DDError();
		}
	}
}
