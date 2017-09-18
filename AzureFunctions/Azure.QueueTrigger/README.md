# Azure QueueTrigger
This sample implements an Azure Function, picking a message from Azure Storage Queue for treatment. It gets a message from Azure Storage Queue with a path to a blob, and process this blob. It is set to process only one message at a time.

Functionnalities:
1. Azure Storage Queue
2. Azure Storage Client
3. host.json in Azure Function