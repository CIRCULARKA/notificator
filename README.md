# Notificator

Notifications service.

# Domain logic

## Notification

```mermaid
classDiagram
    User --* Notification

    class Notification {
        +User Owner
        +String Header
        +String Text
        +DateTime StartTime
        +DateTime EndTime
        +TimeStamp Interval
        +int MaxAmount
        +int[] DaysOfTheWeek
        +bool Completed
    }

    class User {
        +String Name
        +AccountData
    }
```
* `Owner`. Each notification is owned by a user
* `Header`. Header of a notification
* `Text`. Text of a notification
* `StartTime`. First time notification should be sent
* `EndTime`. Time after which notification should not be sent
* `Interval`. How often notification should be sent
* `MaxAmount`. Another way to restrict notification sending. Sets maximum amount of times that notification can be sent
* `DaysOfTheWeek`. Sets days of the week in which notification can be sent
* `Completed`. Defines whether a notification is completed (i.e. notification was sent all possible times)

## Processing notifications
```mermaid
classDiagram
    scanning ..> storage
    scanning ..> fetching
    scanning ..> history
    scanning ..> inspector
    interval_scanning ..|> scanning

    user --> validator : Get/Create/Update/Delete notification

    validator --> storage : If validation successful
    default_validator ..|> validator


    default_fetching ..|> fetching
    expired_fetching ..|> fetching

    class user["user"] { }
    class validator["INotificationValidator"] {
        +Validate(NotificationDto)
    }
    class default_validator["DefaultNotificationValidator"] {
        +Validate(NotificationDto)
    }
    class history["INotificationHistory"] {
        +GetHistory(Notification) List&ltSentNotification&gt
        +Add(Notification)
    }
    class scanning["IScanningStrategy"] {
        +void Scan()
    }
    class interval_scanning["IntervalBasedScanningStrategy"] {
        +void Scan()
    }
    class fetching["IFetchingStrategy"] {
        +Fetch() List&ltNotification&gt
    }
    class default_fetching["DefaultFetchingStrategy"] {
        +Fetch() List&ltNotification&gt
    }
    class expired_fetching["ExpiredFetchingStrategy"] {
        +Fetch() List&ltNotification&gt
    }
    class storage["INotificationStorage"] {
        +Get(Func&ltNotification, bool&gt) List&ltNotification&gt
        +Add(NotificationDto)
        +Update(NotificationDto)
        +Delete(NotificationDto)
    }
    class inspector["INotificationInspector"] {
        +ShouldBeCompleted(Notification) bool
    }
```
