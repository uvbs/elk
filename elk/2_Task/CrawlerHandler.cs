namespace Elk.Task
{
  using Elk.DataTypes.Interfaces;
  using Abot.Crawler;
  using Abot.Poco;
  using System;
  using System.Collections.Generic;
  using System.Net;
  using System.Text.RegularExpressions;
  using System.Threading;


  public class CrawlerHandler : IObservableCrawler, IObservableReferer
  {

    #region MEMBERS

    private List<IObserverCrawler> observersCrawler;
    private List<IObserverReferer> observersReferers;
    private PoliteWebCrawler crawler;
    private string hostName;
    private CancellationTokenSource cancellationTokenSource;
    private Dictionary<string, string> foundHostNames;

    #endregion


    #region PUBLIC

    public CrawlerHandler()
    {
      this.observersCrawler = new List<IObserverCrawler>();
      this.observersReferers = new List<IObserverReferer>();
      this.foundHostNames = new Dictionary<string, string>();
    }

    public void StartCrawling(string hostName)
    {
      string url = string.Format("http://{0}", hostName);

      this.hostName = hostName; 
      this.foundHostNames.Clear();
      this.cancellationTokenSource = new CancellationTokenSource();
      this.crawler = new PoliteWebCrawler();

      //this.crawler.PageCrawlStartingAsync += crawler_ProcessPageCrawlStarting;
      this.crawler.PageCrawlCompletedAsync += crawler_ProcessPageCrawlCompleted;
      //this.crawler.PageCrawlDisallowedAsync += crawler_PageCrawlDisallowed;
      //this.crawler.PageLinksCrawlDisallowedAsync += crawler_PageLinksCrawlDisallowed;

      CrawlResult result = crawler.Crawl(new Uri(url), this.cancellationTokenSource); //This is synchronous, it will not go to the next line until the crawl has completed
      
      if (result.ErrorOccurred)
      {
        this.NotifyCrawler(string.Format("Crawl of {0} completed with error: {1}", result.RootUri.AbsoluteUri, result.ErrorException.Message));
      }
      else
      {
        this.NotifyCrawler(string.Format("Crawl of {0} completed without error.", result.RootUri.AbsoluteUri));
      }
    }

    public void Cancel()
    {
      this.cancellationTokenSource.Cancel();
    }

    #endregion 


    #region PRIVATE

    private void crawler_ProcessPageCrawlStarting(object sender, PageCrawlStartingArgs e)
    {
      PageToCrawl pageToCrawl = e.PageToCrawl;
      this.NotifyCrawler(string.Format("About to crawl link {0} which was found on page {1}", pageToCrawl.Uri.AbsoluteUri, pageToCrawl.ParentUri.AbsoluteUri));
    }


    private void crawler_ProcessPageCrawlCompleted(object sender, PageCrawlCompletedArgs e)
    {
      CrawledPage crawledPage = e.CrawledPage;
      if (crawledPage.WebException != null || crawledPage.HttpWebResponse.StatusCode == HttpStatusCode.OK)
      {
        // Notify crawler observers
        this.NotifyCrawler(crawledPage.Uri.AbsoluteUri);

        // Notify referer observers
        if (Regex.Match(crawledPage.Uri.Host, Regex.Escape(this.hostName), RegexOptions.IgnoreCase).Success &&
            !this.foundHostNames.ContainsKey(crawledPage.Uri.Host))
        {
          this.foundHostNames.Add(crawledPage.Uri.Host, "");
          this.NotifyObserverReferer(crawledPage.Uri.Host, "COOKIES");
        }
      }

      //if (crawledPage.WebException != null || crawledPage.HttpWebResponse.StatusCode != HttpStatusCode.OK)
      //{
      //  this.Notify(string.Format("Crawl of page failed {0}", crawledPage.Uri.AbsoluteUri));
      //}
      //else
      //{
      //  this.Notify(string.Format("Crawl of page succeeded {0}", crawledPage.Uri.AbsoluteUri));
      //}

      //if (string.IsNullOrEmpty(crawledPage.Content.Text))
      //{
      //  this.Notify(string.Format("Page had no content {0}", crawledPage.Uri.AbsoluteUri));
      //}

      //var htmlAgilityPackDocument = crawledPage.HtmlDocument; //Html Agility Pack parser
      //var angleSharpHtmlDocument = crawledPage.AngleSharpHtmlDocument; //AngleSharp parser
    }


    private void crawler_PageLinksCrawlDisallowed(object sender, PageLinksCrawlDisallowedArgs e)
    {
      CrawledPage crawledPage = e.CrawledPage;
      this.NotifyCrawler(string.Format("Did not crawl the links on page {0} due to {1}", crawledPage.Uri.AbsoluteUri, e.DisallowedReason));
    }


    private void crawler_PageCrawlDisallowed(object sender, PageCrawlDisallowedArgs e)
    {
      PageToCrawl pageToCrawl = e.PageToCrawl;
      this.NotifyCrawler(string.Format("Did not crawl page {0} due to {1}", pageToCrawl.Uri.AbsoluteUri, e.DisallowedReason));
    }

    #endregion


    #region INTERFACE: IObservable

    public void AddObserverCrawler(IObserverCrawler newObserver)
    {
      this.observersCrawler.Add(newObserver);
    }

    public void NotifyCrawler(string logMessage)
    {
      foreach (IObserverCrawler observer in this.observersCrawler)
      {
        observer.UpdateLog(logMessage);
      }
    }

    #endregion


    #region INTERFACE: IObservableReferer

    public void AddObserverReferer(IObserverReferer observer)
    {
      this.observersReferers.Add(observer);
    }

    public void NotifyObserverReferer(string entryHostName, string cookies)
    {
      if (this.observersReferers == null || this.observersReferers.Count <= 0)
      {
        return;
      }

      if (string.IsNullOrEmpty(entryHostName))
      {
        return;
      }

      foreach (IObserverReferer observer in this.observersReferers)
      {
        observer.UpdateReferer(entryHostName, cookies);
      }
    }

    #endregion

  }
}
