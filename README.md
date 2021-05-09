# Jour.Webhooks

## Pushing image to gcr

Auth to GCP first:

```bash
gcloud config set project helical-patrol-307414
gcloud auth login
gcloud auth configure-docker
```

Build, tag and push:

```bash
docker build -t gcr.io/helical-patrol-307414/jour.webhooks .
docker push gcr.io/helical-patrol-307414/jour.webhooks
```

## Running in GKE

```bash
cd deploy
kubectl kubectl apply -f deployment.yaml
```

## Creating secret

```bash
kubectl create secret generic jour-webhooks-secrets \
--namespace=mialkin \
--from-literal=telegram-endpoints='Controller1=BotAPIKey1;Controller2=BotAPIKey2'
```

## Set bot webhook

Run this in web browser:

```text
https://api.telegram.org/bot<YOUR_TOKEN>/setWebhook?url=WEBHOOK_URL
```
