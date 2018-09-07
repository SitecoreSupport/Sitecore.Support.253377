namespace Sitecore.Support.XA.Feature.SiteMetadata.Pipelines.HttpRequestBegin
{
  using System;
  using System.Web;
  using System.Web.Caching;

  public class SitemapHandler : Sitecore.XA.Feature.SiteMetadata.Pipelines.HttpRequestBegin.SitemapHandler
  {
    protected new string CacheKey
    {
      get
      {
        #region Changed code
        return string.Format("{0}/{1}/{2}/{3}", "XA-SITEMAP", (this.Context.Database == null) ? null : this.Context.Database.Name, this.CurrentSite.Name, HttpContext.Current.Request.Url); 
        #endregion
      }
    }

    protected override string GetSitemapFromCache()
    {
      string str = null;
      if (HttpRuntime.Cache[this.CacheKey] != null)
      {
        str = HttpRuntime.Cache.Get(this.CacheKey) as string;
      }
      return str;
    }

    protected override void StoreSitemapInCache(string sitemap, string cacheKey)
    {
      HttpRuntime.Cache.Insert(this.CacheKey, sitemap, null, DateTime.UtcNow.AddMinutes((double)this.CacheExpiration), Cache.NoSlidingExpiration);
    }
  }
}