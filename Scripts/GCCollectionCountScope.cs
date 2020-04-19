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
		public delegate void OnCompleteCallback( string name, int count );

		//==============================================================================
		// 変数(readonly)
		//==============================================================================
		private readonly string             m_name;
		private readonly int                m_startCount;
		private readonly OnCompleteCallback m_onComplete;

		//==============================================================================
		// イベント(static)
		//==============================================================================
		public static event OnCompleteCallback OnComplete;

		//==============================================================================
		// 関数
		//==============================================================================
		/// <summary>
		/// 計測を開始します
		/// </summary>
		public GCCollectionCountScope( string name, OnCompleteCallback onComplete )
		{
			m_name       = name;
			m_startCount = GC.CollectionCount( 0 );
			m_onComplete = onComplete;
		}

		/// <summary>
		/// 計測を開始します
		/// </summary>
		public GCCollectionCountScope( string name ) : this( name, null )
		{
		}

		/// <summary>
		/// 計測を終了します
		/// </summary>
		public void Dispose()
		{
			var count = GC.CollectionCount( 0 ) - m_startCount;
			m_onComplete?.Invoke( m_name, count );
			OnComplete?.Invoke( m_name, count );
		}
	}
}