### Example 1: Get the revision of a reservation
```powershell
Get-AzReservationHistory -ReservationId 2ef560a7-f469-4b62-87b7-5312d588ce2a -ReservationOrderId 2b9b9372-24e1-4a07-a354-2078fe347cf9
```

```output
Location ReservationOrderId/ReservationId                                            Sku           State             BenefitStartTime      ExpiryDate            LastUpdatedDateTime   SkuDescription
-------- --------------------------------                                            ---           -----             ----------------      ----------            -------------------   --------------
westus   2b9b9372-24e1-4a07-a354-2078fe347cf9/2ef560a7-f469-4b62-87b7-5312d588ce2a/9 Standard_B1ls Succeeded         6/24/2022 10:06:39 PM 6/24/2023 12:00:00 AM 6/24/2022 10:06:43 PM Reserved VM Instance, Standard_B1ls, US West,… 
westus   2b9b9372-24e1-4a07-a354-2078fe347cf9/2ef560a7-f469-4b62-87b7-5312d588ce2a/8 Standard_B1ls Succeeded         6/24/2022 10:06:39 PM 6/24/2023 12:00:00 AM 6/24/2022 10:06:43 PM Reserved VM Instance, Standard_B1ls, US West,… 
westus   2b9b9372-24e1-4a07-a354-2078fe347cf9/2ef560a7-f469-4b62-87b7-5312d588ce2a/7 Standard_B1ls ConfirmedBilling                                              6/24/2022 10:06:17 PM Reserved VM Instance, Standard_B1ls, US West,…
westus   2b9b9372-24e1-4a07-a354-2078fe347cf9/2ef560a7-f469-4b62-87b7-5312d588ce2a/6 Standard_B1ls PendingBilling                                                6/24/2022 10:04:04 PM Reserved VM Instance, Standard_B1ls, US West,… 
westus   2b9b9372-24e1-4a07-a354-2078fe347cf9/2ef560a7-f469-4b62-87b7-5312d588ce2a/5 Standard_B1ls ConfirmedCapacity                                             6/24/2022 10:03:44 PM Reserved VM Instance, Standard_B1ls, US West,… 
westus   2b9b9372-24e1-4a07-a354-2078fe347cf9/2ef560a7-f469-4b62-87b7-5312d588ce2a/4 Standard_B1ls PendingCapacity                                               6/24/2022 10:03:34 PM Reserved VM Instance, Standard_B1ls, US West,… 
westus   2b9b9372-24e1-4a07-a354-2078fe347cf9/2ef560a7-f469-4b62-87b7-5312d588ce2a/3 Standard_B1ls Creating                                                      6/24/2022 10:03:17 PM Reserved VM Instance, Standard_B1ls, US West,… 
westus   2b9b9372-24e1-4a07-a354-2078fe347cf9/2ef560a7-f469-4b62-87b7-5312d588ce2a/2 Standard_B1ls Creating                                                      6/24/2022 10:03:04 PM Reserved VM Instance, Standard_B1ls, US West,… 
westus   2b9b9372-24e1-4a07-a354-2078fe347cf9/2ef560a7-f469-4b62-87b7-5312d588ce2a/1 Standard_B1ls Creating                                                      6/24/2022 10:02:52 PM Reserved VM Instance, Standard_B1ls, US West,… 
```

Get the revision of a reservation. Some data might be truncated due to the width of powershell view, appending this to the end of the command to show the truncated data: | ft -Wrap



