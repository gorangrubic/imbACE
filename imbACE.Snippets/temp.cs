private plugIn_dataLoader _analytics;
/// <summary>
/// Plugin for post-processing of exported DataTable files
/// </summary>
public plugIn_dataLoader analytics
{
    get {

        if (_analytics == null) _analytics = new plugIn_dataLoader(this, nameof(analytics));
        return _analytics;
    }
    set { _analytics = value; }
}