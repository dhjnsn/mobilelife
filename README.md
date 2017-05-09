# MobileLife

The following API is exposed.

**PUT /api/municipalities**
Replaces the entire municipalities collection. Use for importing data from a json file.
```
curl -X PUT -d @data.json -H "Content-Type:application/json" \
http://host/api/municipalities
```
 
**GET /api/municipalities**
List all municipalities and taxes scheduled.
```
curl http://host/api/municipalities
```

**PUT /api/municipalities/_{municipality}_/_{start}_/_{duration}_**
Insert a new scheduled tax record in _{municipality}_. The period is defined by the _{start}_ date and _{duration}_ days. The HTTP request body contains the tax value.
```
curl -i -H "Content-Type:application/json" -X PUT -d "0.4" \
http://host/api/municipalities/london/taxes/2017-05-09/1
```

**GET /api/appliedtaxes/_{municipality}_/_{date}_**
Retrieve a specific municipality tax for _{municipality}_ on _{date}_.
```
curl http://host/api/appiedtaxes/vilnius/2016-05-02
```
