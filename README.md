# leca - Lawyer Email Calendar Assistant


This is the intial teams integration branch.

## Graph API
Endpoints to use for calendar integration:
```GET https://graph.microsoft.com/v1.0/me/calendarview?startdatetime={YYYY-MM-DDTHH:MM:SS}&enddatetime={YYYY-MM-DDTHH:MM:SS} ```

track changes
```GET https://graph.microsoft.com/v1.0/me/calendarview?delta?startdatetime={YYYY-MM-DDTHH:MM:SS}&enddatetime={YYYY-MM-DDTHH:MM:SS} ```

Endpoints for checking emails with keywords:
```GET  https://graph.microsoft.com/v1.0/me/messages?$search="{text}"```

---