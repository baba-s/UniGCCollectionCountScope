using System;

namespace UniGCCollectionCountScope
{
	/// <summary>
	/// GC が何回発生したか計測するクラス
	/// </summary>
	public sealed class GCCollectionCountScope : IDisposable
	{
		//==============================================================================
		// デリゲート
		//==============================================================================
		public delegate void OnStartCallback( string name );

		public delegate void OnCompleteCallback( string name, int count );

		//==============================================================================
		// 変数(readonly)
		//==============================================================================
		private readonly string m_name;
		private readonly int    m_startCount;

		//==============================================================================
		// イベント(static)
		//==============================================================================
		public static event OnStartCallback    OnStart;
		public static event OnCompleteCallback OnComplete;

		//==============================================================================
		// 関数
		//==============================================================================
		/// <summary>
		/// 計測を開始します
		/// </summary>
		public GCCollectionCountScope( string name )
		{
			m_name       = name;
			m_startCount = GC.CollectionCount( 0 );
			OnStart?.Invoke( name );
		}

		/// <summary>
		/// 計測を終了します
		/// </summary>
		public void Dispose()
		{
			var count = GC.CollectionCount( 0 ) - m_startCount;
			OnComplete?.Invoke( m_name, count );
		}
	}
}