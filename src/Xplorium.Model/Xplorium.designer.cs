﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18010
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Xplorium.Models
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="Xplorium")]
	public partial class XploriumDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertCache(Cache instance);
    partial void UpdateCache(Cache instance);
    partial void DeleteCache(Cache instance);
    partial void InsertWord(Word instance);
    partial void UpdateWord(Word instance);
    partial void DeleteWord(Word instance);
    partial void InsertHit(Hit instance);
    partial void UpdateHit(Hit instance);
    partial void DeleteHit(Hit instance);
    partial void InsertTrack(Track instance);
    partial void UpdateTrack(Track instance);
    partial void DeleteTrack(Track instance);
    partial void InsertUrl(Url instance);
    partial void UpdateUrl(Url instance);
    partial void DeleteUrl(Url instance);
    partial void InsertParsedContent(ParsedContent instance);
    partial void UpdateParsedContent(ParsedContent instance);
    partial void DeleteParsedContent(ParsedContent instance);
    #endregion
		
		public XploriumDataContext() : 
				base(global::Xplorium.Models.Properties.Settings.Default.Xplorium__Stable_ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public XploriumDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public XploriumDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public XploriumDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public XploriumDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Cache> Caches
		{
			get
			{
				return this.GetTable<Cache>();
			}
		}
		
		public System.Data.Linq.Table<Word> Words
		{
			get
			{
				return this.GetTable<Word>();
			}
		}
		
		public System.Data.Linq.Table<Hit> Hits
		{
			get
			{
				return this.GetTable<Hit>();
			}
		}
		
		public System.Data.Linq.Table<Track> Tracks
		{
			get
			{
				return this.GetTable<Track>();
			}
		}
		
		public System.Data.Linq.Table<Url> Urls
		{
			get
			{
				return this.GetTable<Url>();
			}
		}
		
		public System.Data.Linq.Table<ParsedContent> ParsedContents
		{
			get
			{
				return this.GetTable<ParsedContent>();
			}
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.GetCacheableUrls")]
		public ISingleResult<Url> GetCacheableUrls([global::System.Data.Linq.Mapping.ParameterAttribute(DbType="Int")] System.Nullable<int> count)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), count);
			return ((ISingleResult<Url>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.UnlockLockedUrls")]
		public int UnlockLockedUrls()
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
			return ((int)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.GetParsableCaches")]
		public ISingleResult<Cache> GetParsableCaches([global::System.Data.Linq.Mapping.ParameterAttribute(DbType="Int")] System.Nullable<int> count)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), count);
			return ((ISingleResult<Cache>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.UnlockLockedCaches")]
		public int UnlockLockedCaches()
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
			return ((int)(result.ReturnValue));
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Caches")]
	public partial class Cache : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private System.Guid _CacheId;
		
		private System.DateTime _CachedOn;
		
		private bool _Locked;
		
		private string _Response;
		
		private long _Length;
		
		private EntityRef<ParsedContent> _ParsedContent;
		
		private EntityRef<Url> _Url;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnCacheIdChanging(System.Guid value);
    partial void OnCacheIdChanged();
    partial void OnCachedOnChanging(System.DateTime value);
    partial void OnCachedOnChanged();
    partial void OnLockedChanging(bool value);
    partial void OnLockedChanged();
    partial void OnResponseChanging(string value);
    partial void OnResponseChanged();
    partial void OnLengthChanging(long value);
    partial void OnLengthChanged();
    #endregion
		
		public Cache()
		{
			this._ParsedContent = default(EntityRef<ParsedContent>);
			this._Url = default(EntityRef<Url>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CacheId", DbType="UniqueIdentifier NOT NULL", IsPrimaryKey=true)]
		public System.Guid CacheId
		{
			get
			{
				return this._CacheId;
			}
			set
			{
				if ((this._CacheId != value))
				{
					if (this._Url.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnCacheIdChanging(value);
					this.SendPropertyChanging();
					this._CacheId = value;
					this.SendPropertyChanged("CacheId");
					this.OnCacheIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CachedOn", DbType="Date NOT NULL")]
		public System.DateTime CachedOn
		{
			get
			{
				return this._CachedOn;
			}
			set
			{
				if ((this._CachedOn != value))
				{
					this.OnCachedOnChanging(value);
					this.SendPropertyChanging();
					this._CachedOn = value;
					this.SendPropertyChanged("CachedOn");
					this.OnCachedOnChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Locked", DbType="Bit NOT NULL")]
		public bool Locked
		{
			get
			{
				return this._Locked;
			}
			set
			{
				if ((this._Locked != value))
				{
					this.OnLockedChanging(value);
					this.SendPropertyChanging();
					this._Locked = value;
					this.SendPropertyChanged("Locked");
					this.OnLockedChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Response", DbType="NVarChar(MAX) NOT NULL", CanBeNull=false)]
		public string Response
		{
			get
			{
				return this._Response;
			}
			set
			{
				if ((this._Response != value))
				{
					this.OnResponseChanging(value);
					this.SendPropertyChanging();
					this._Response = value;
					this.SendPropertyChanged("Response");
					this.OnResponseChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Length", DbType="BigInt NOT NULL")]
		public long Length
		{
			get
			{
				return this._Length;
			}
			set
			{
				if ((this._Length != value))
				{
					this.OnLengthChanging(value);
					this.SendPropertyChanging();
					this._Length = value;
					this.SendPropertyChanged("Length");
					this.OnLengthChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Cache_ParsedContent", Storage="_ParsedContent", ThisKey="CacheId", OtherKey="ParsedContentId", IsUnique=true, IsForeignKey=false)]
		public ParsedContent ParsedContent
		{
			get
			{
				return this._ParsedContent.Entity;
			}
			set
			{
				ParsedContent previousValue = this._ParsedContent.Entity;
				if (((previousValue != value) 
							|| (this._ParsedContent.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._ParsedContent.Entity = null;
						previousValue.Cache = null;
					}
					this._ParsedContent.Entity = value;
					if ((value != null))
					{
						value.Cache = this;
					}
					this.SendPropertyChanged("ParsedContent");
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Url_Cache", Storage="_Url", ThisKey="CacheId", OtherKey="UrlId", IsForeignKey=true)]
		public Url Url
		{
			get
			{
				return this._Url.Entity;
			}
			set
			{
				Url previousValue = this._Url.Entity;
				if (((previousValue != value) 
							|| (this._Url.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Url.Entity = null;
						previousValue.Cache = null;
					}
					this._Url.Entity = value;
					if ((value != null))
					{
						value.Cache = this;
						this._CacheId = value.UrlId;
					}
					else
					{
						this._CacheId = default(System.Guid);
					}
					this.SendPropertyChanged("Url");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Words")]
	public partial class Word : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private System.Guid _WordId;
		
		private string _Text;
		
		private EntitySet<Hit> _Hits;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnWordIdChanging(System.Guid value);
    partial void OnWordIdChanged();
    partial void OnTextChanging(string value);
    partial void OnTextChanged();
    #endregion
		
		public Word()
		{
			this._Hits = new EntitySet<Hit>(new Action<Hit>(this.attach_Hits), new Action<Hit>(this.detach_Hits));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_WordId", DbType="UniqueIdentifier NOT NULL", IsPrimaryKey=true, IsDbGenerated=true)]
		public System.Guid WordId
		{
			get
			{
				return this._WordId;
			}
			set
			{
				if ((this._WordId != value))
				{
					this.OnWordIdChanging(value);
					this.SendPropertyChanging();
					this._WordId = value;
					this.SendPropertyChanged("WordId");
					this.OnWordIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Text", DbType="NVarChar(64) NOT NULL", CanBeNull=false)]
		public string Text
		{
			get
			{
				return this._Text;
			}
			set
			{
				if ((this._Text != value))
				{
					this.OnTextChanging(value);
					this.SendPropertyChanging();
					this._Text = value;
					this.SendPropertyChanged("Text");
					this.OnTextChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Word_Hit", Storage="_Hits", ThisKey="WordId", OtherKey="WordId")]
		public EntitySet<Hit> Hits
		{
			get
			{
				return this._Hits;
			}
			set
			{
				this._Hits.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_Hits(Hit entity)
		{
			this.SendPropertyChanging();
			entity.Word = this;
		}
		
		private void detach_Hits(Hit entity)
		{
			this.SendPropertyChanging();
			entity.Word = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Hits")]
	public partial class Hit : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private System.Guid _ParsedContentId;
		
		private System.Guid _WordId;
		
		private int _Count;
		
		private EntityRef<Word> _Word;
		
		private EntityRef<ParsedContent> _ParsedContent;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnParsedContentIdChanging(System.Guid value);
    partial void OnParsedContentIdChanged();
    partial void OnWordIdChanging(System.Guid value);
    partial void OnWordIdChanged();
    partial void OnCountChanging(int value);
    partial void OnCountChanged();
    #endregion
		
		public Hit()
		{
			this._Word = default(EntityRef<Word>);
			this._ParsedContent = default(EntityRef<ParsedContent>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ParsedContentId", DbType="UniqueIdentifier NOT NULL", IsPrimaryKey=true)]
		public System.Guid ParsedContentId
		{
			get
			{
				return this._ParsedContentId;
			}
			set
			{
				if ((this._ParsedContentId != value))
				{
					if (this._ParsedContent.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnParsedContentIdChanging(value);
					this.SendPropertyChanging();
					this._ParsedContentId = value;
					this.SendPropertyChanged("ParsedContentId");
					this.OnParsedContentIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_WordId", DbType="UniqueIdentifier NOT NULL", IsPrimaryKey=true)]
		public System.Guid WordId
		{
			get
			{
				return this._WordId;
			}
			set
			{
				if ((this._WordId != value))
				{
					if (this._Word.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnWordIdChanging(value);
					this.SendPropertyChanging();
					this._WordId = value;
					this.SendPropertyChanged("WordId");
					this.OnWordIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Count", DbType="Int NOT NULL")]
		public int Count
		{
			get
			{
				return this._Count;
			}
			set
			{
				if ((this._Count != value))
				{
					this.OnCountChanging(value);
					this.SendPropertyChanging();
					this._Count = value;
					this.SendPropertyChanged("Count");
					this.OnCountChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Word_Hit", Storage="_Word", ThisKey="WordId", OtherKey="WordId", IsForeignKey=true)]
		public Word Word
		{
			get
			{
				return this._Word.Entity;
			}
			set
			{
				Word previousValue = this._Word.Entity;
				if (((previousValue != value) 
							|| (this._Word.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Word.Entity = null;
						previousValue.Hits.Remove(this);
					}
					this._Word.Entity = value;
					if ((value != null))
					{
						value.Hits.Add(this);
						this._WordId = value.WordId;
					}
					else
					{
						this._WordId = default(System.Guid);
					}
					this.SendPropertyChanged("Word");
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="ParsedContent_Hit", Storage="_ParsedContent", ThisKey="ParsedContentId", OtherKey="ParsedContentId", IsForeignKey=true)]
		public ParsedContent ParsedContent
		{
			get
			{
				return this._ParsedContent.Entity;
			}
			set
			{
				ParsedContent previousValue = this._ParsedContent.Entity;
				if (((previousValue != value) 
							|| (this._ParsedContent.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._ParsedContent.Entity = null;
						previousValue.Hits.Remove(this);
					}
					this._ParsedContent.Entity = value;
					if ((value != null))
					{
						value.Hits.Add(this);
						this._ParsedContentId = value.ParsedContentId;
					}
					else
					{
						this._ParsedContentId = default(System.Guid);
					}
					this.SendPropertyChanged("ParsedContent");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Tracks")]
	public partial class Track : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private System.Guid _TrackId;
		
		private System.DateTime _TrackedOn;
		
		private string _Username;
		
		private string _RawQuery;
		
		private string _IpAddress;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnTrackIdChanging(System.Guid value);
    partial void OnTrackIdChanged();
    partial void OnTrackedOnChanging(System.DateTime value);
    partial void OnTrackedOnChanged();
    partial void OnUsernameChanging(string value);
    partial void OnUsernameChanged();
    partial void OnRawQueryChanging(string value);
    partial void OnRawQueryChanged();
    partial void OnIpAddressChanging(string value);
    partial void OnIpAddressChanged();
    #endregion
		
		public Track()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TrackId", DbType="UniqueIdentifier NOT NULL", IsPrimaryKey=true, IsDbGenerated=true)]
		public System.Guid TrackId
		{
			get
			{
				return this._TrackId;
			}
			set
			{
				if ((this._TrackId != value))
				{
					this.OnTrackIdChanging(value);
					this.SendPropertyChanging();
					this._TrackId = value;
					this.SendPropertyChanged("TrackId");
					this.OnTrackIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TrackedOn", DbType="Date NOT NULL")]
		public System.DateTime TrackedOn
		{
			get
			{
				return this._TrackedOn;
			}
			set
			{
				if ((this._TrackedOn != value))
				{
					this.OnTrackedOnChanging(value);
					this.SendPropertyChanging();
					this._TrackedOn = value;
					this.SendPropertyChanged("TrackedOn");
					this.OnTrackedOnChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Username", DbType="NVarChar(16) NOT NULL", CanBeNull=false)]
		public string Username
		{
			get
			{
				return this._Username;
			}
			set
			{
				if ((this._Username != value))
				{
					this.OnUsernameChanging(value);
					this.SendPropertyChanging();
					this._Username = value;
					this.SendPropertyChanged("Username");
					this.OnUsernameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RawQuery", DbType="NVarChar(256) NOT NULL", CanBeNull=false)]
		public string RawQuery
		{
			get
			{
				return this._RawQuery;
			}
			set
			{
				if ((this._RawQuery != value))
				{
					this.OnRawQueryChanging(value);
					this.SendPropertyChanging();
					this._RawQuery = value;
					this.SendPropertyChanged("RawQuery");
					this.OnRawQueryChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IpAddress", DbType="NVarChar(15) NOT NULL", CanBeNull=false)]
		public string IpAddress
		{
			get
			{
				return this._IpAddress;
			}
			set
			{
				if ((this._IpAddress != value))
				{
					this.OnIpAddressChanging(value);
					this.SendPropertyChanging();
					this._IpAddress = value;
					this.SendPropertyChanged("IpAddress");
					this.OnIpAddressChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Urls")]
	public partial class Url : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private System.Guid _UrlId;
		
		private System.DateTime _FollowedOn;
		
		private bool _Approved;
		
		private bool _Locked;
		
		private string _DetectedPath;
		
		private string _ResolvedPath;
		
		private double _Rate;
		
		private EntityRef<Cache> _Cache;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnUrlIdChanging(System.Guid value);
    partial void OnUrlIdChanged();
    partial void OnFollowedOnChanging(System.DateTime value);
    partial void OnFollowedOnChanged();
    partial void OnApprovedChanging(bool value);
    partial void OnApprovedChanged();
    partial void OnLockedChanging(bool value);
    partial void OnLockedChanged();
    partial void OnDetectedPathChanging(string value);
    partial void OnDetectedPathChanged();
    partial void OnResolvedPathChanging(string value);
    partial void OnResolvedPathChanged();
    partial void OnRateChanging(double value);
    partial void OnRateChanged();
    #endregion
		
		public Url()
		{
			this._Cache = default(EntityRef<Cache>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UrlId", DbType="UniqueIdentifier NOT NULL", IsPrimaryKey=true, IsDbGenerated=true)]
		public System.Guid UrlId
		{
			get
			{
				return this._UrlId;
			}
			set
			{
				if ((this._UrlId != value))
				{
					this.OnUrlIdChanging(value);
					this.SendPropertyChanging();
					this._UrlId = value;
					this.SendPropertyChanged("UrlId");
					this.OnUrlIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FollowedOn", DbType="Date NOT NULL")]
		public System.DateTime FollowedOn
		{
			get
			{
				return this._FollowedOn;
			}
			set
			{
				if ((this._FollowedOn != value))
				{
					this.OnFollowedOnChanging(value);
					this.SendPropertyChanging();
					this._FollowedOn = value;
					this.SendPropertyChanged("FollowedOn");
					this.OnFollowedOnChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Approved", DbType="Bit NOT NULL")]
		public bool Approved
		{
			get
			{
				return this._Approved;
			}
			set
			{
				if ((this._Approved != value))
				{
					this.OnApprovedChanging(value);
					this.SendPropertyChanging();
					this._Approved = value;
					this.SendPropertyChanged("Approved");
					this.OnApprovedChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Locked", DbType="Bit NOT NULL")]
		public bool Locked
		{
			get
			{
				return this._Locked;
			}
			set
			{
				if ((this._Locked != value))
				{
					this.OnLockedChanging(value);
					this.SendPropertyChanging();
					this._Locked = value;
					this.SendPropertyChanged("Locked");
					this.OnLockedChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DetectedPath", DbType="NVarChar(2048) NOT NULL", CanBeNull=false)]
		public string DetectedPath
		{
			get
			{
				return this._DetectedPath;
			}
			set
			{
				if ((this._DetectedPath != value))
				{
					this.OnDetectedPathChanging(value);
					this.SendPropertyChanging();
					this._DetectedPath = value;
					this.SendPropertyChanged("DetectedPath");
					this.OnDetectedPathChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ResolvedPath", DbType="NVarChar(2048) NOT NULL", CanBeNull=false)]
		public string ResolvedPath
		{
			get
			{
				return this._ResolvedPath;
			}
			set
			{
				if ((this._ResolvedPath != value))
				{
					this.OnResolvedPathChanging(value);
					this.SendPropertyChanging();
					this._ResolvedPath = value;
					this.SendPropertyChanged("ResolvedPath");
					this.OnResolvedPathChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Rate", DbType="Float NOT NULL")]
		public double Rate
		{
			get
			{
				return this._Rate;
			}
			set
			{
				if ((this._Rate != value))
				{
					this.OnRateChanging(value);
					this.SendPropertyChanging();
					this._Rate = value;
					this.SendPropertyChanged("Rate");
					this.OnRateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Url_Cache", Storage="_Cache", ThisKey="UrlId", OtherKey="CacheId", IsUnique=true, IsForeignKey=false)]
		public Cache Cache
		{
			get
			{
				return this._Cache.Entity;
			}
			set
			{
				Cache previousValue = this._Cache.Entity;
				if (((previousValue != value) 
							|| (this._Cache.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Cache.Entity = null;
						previousValue.Url = null;
					}
					this._Cache.Entity = value;
					if ((value != null))
					{
						value.Url = this;
					}
					this.SendPropertyChanged("Cache");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.ParsedContents")]
	public partial class ParsedContent : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private System.Guid _ParsedContentId;
		
		private System.DateTime _ParsedOn;
		
		private string _Title;
		
		private string _Description;
		
		private string _Keywords;
		
		private bool _CanBeIndexed;
		
		private bool _CanBeFollowed;
		
		private EntitySet<Hit> _Hits;
		
		private EntityRef<Cache> _Cache;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnParsedContentIdChanging(System.Guid value);
    partial void OnParsedContentIdChanged();
    partial void OnParsedOnChanging(System.DateTime value);
    partial void OnParsedOnChanged();
    partial void OnTitleChanging(string value);
    partial void OnTitleChanged();
    partial void OnDescriptionChanging(string value);
    partial void OnDescriptionChanged();
    partial void OnKeywordsChanging(string value);
    partial void OnKeywordsChanged();
    partial void OnCanBeIndexedChanging(bool value);
    partial void OnCanBeIndexedChanged();
    partial void OnCanBeFollowedChanging(bool value);
    partial void OnCanBeFollowedChanged();
    #endregion
		
		public ParsedContent()
		{
			this._Hits = new EntitySet<Hit>(new Action<Hit>(this.attach_Hits), new Action<Hit>(this.detach_Hits));
			this._Cache = default(EntityRef<Cache>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ParsedContentId", DbType="UniqueIdentifier NOT NULL", IsPrimaryKey=true)]
		public System.Guid ParsedContentId
		{
			get
			{
				return this._ParsedContentId;
			}
			set
			{
				if ((this._ParsedContentId != value))
				{
					if (this._Cache.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnParsedContentIdChanging(value);
					this.SendPropertyChanging();
					this._ParsedContentId = value;
					this.SendPropertyChanged("ParsedContentId");
					this.OnParsedContentIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ParsedOn", DbType="Date NOT NULL")]
		public System.DateTime ParsedOn
		{
			get
			{
				return this._ParsedOn;
			}
			set
			{
				if ((this._ParsedOn != value))
				{
					this.OnParsedOnChanging(value);
					this.SendPropertyChanging();
					this._ParsedOn = value;
					this.SendPropertyChanged("ParsedOn");
					this.OnParsedOnChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Title", DbType="NVarChar(72)")]
		public string Title
		{
			get
			{
				return this._Title;
			}
			set
			{
				if ((this._Title != value))
				{
					this.OnTitleChanging(value);
					this.SendPropertyChanging();
					this._Title = value;
					this.SendPropertyChanged("Title");
					this.OnTitleChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Description", DbType="NVarChar(256)")]
		public string Description
		{
			get
			{
				return this._Description;
			}
			set
			{
				if ((this._Description != value))
				{
					this.OnDescriptionChanging(value);
					this.SendPropertyChanging();
					this._Description = value;
					this.SendPropertyChanged("Description");
					this.OnDescriptionChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Keywords", DbType="NVarChar(180)")]
		public string Keywords
		{
			get
			{
				return this._Keywords;
			}
			set
			{
				if ((this._Keywords != value))
				{
					this.OnKeywordsChanging(value);
					this.SendPropertyChanging();
					this._Keywords = value;
					this.SendPropertyChanged("Keywords");
					this.OnKeywordsChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CanBeIndexed", DbType="Bit NOT NULL")]
		public bool CanBeIndexed
		{
			get
			{
				return this._CanBeIndexed;
			}
			set
			{
				if ((this._CanBeIndexed != value))
				{
					this.OnCanBeIndexedChanging(value);
					this.SendPropertyChanging();
					this._CanBeIndexed = value;
					this.SendPropertyChanged("CanBeIndexed");
					this.OnCanBeIndexedChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CanBeFollowed", DbType="Bit NOT NULL")]
		public bool CanBeFollowed
		{
			get
			{
				return this._CanBeFollowed;
			}
			set
			{
				if ((this._CanBeFollowed != value))
				{
					this.OnCanBeFollowedChanging(value);
					this.SendPropertyChanging();
					this._CanBeFollowed = value;
					this.SendPropertyChanged("CanBeFollowed");
					this.OnCanBeFollowedChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="ParsedContent_Hit", Storage="_Hits", ThisKey="ParsedContentId", OtherKey="ParsedContentId")]
		public EntitySet<Hit> Hits
		{
			get
			{
				return this._Hits;
			}
			set
			{
				this._Hits.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Cache_ParsedContent", Storage="_Cache", ThisKey="ParsedContentId", OtherKey="CacheId", IsForeignKey=true)]
		public Cache Cache
		{
			get
			{
				return this._Cache.Entity;
			}
			set
			{
				Cache previousValue = this._Cache.Entity;
				if (((previousValue != value) 
							|| (this._Cache.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Cache.Entity = null;
						previousValue.ParsedContent = null;
					}
					this._Cache.Entity = value;
					if ((value != null))
					{
						value.ParsedContent = this;
						this._ParsedContentId = value.CacheId;
					}
					else
					{
						this._ParsedContentId = default(System.Guid);
					}
					this.SendPropertyChanged("Cache");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_Hits(Hit entity)
		{
			this.SendPropertyChanging();
			entity.ParsedContent = this;
		}
		
		private void detach_Hits(Hit entity)
		{
			this.SendPropertyChanging();
			entity.ParsedContent = null;
		}
	}
}
#pragma warning restore 1591
