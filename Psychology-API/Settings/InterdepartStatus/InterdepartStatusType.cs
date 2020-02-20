namespace Psychology_API.Settings.InterdepartStatus
{
    /// <summary>
    /// Статусы межведоственных запросов.
    /// </summary>
    public enum InterdepartStatusType
    {
        // Ожидает отправки.
        AwaitingDispatch = 1,
        // Запрос отправлен.
        RequestHasBeenSent = 2,
        // Подтверждено.
        Confirmed = 4,
        // Отказано.
        Denied = 5
    }
}