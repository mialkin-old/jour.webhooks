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

## Creating secret

```bash
kubectl create secret generic jour-webhooks-secrets \
--namespace=mialkin \
--from-literal=telegram-endpoints='Controller1=BotAPIKey1;Controller2=BotAPIKey2' \
--from-literal=hostname='mialkin-rabbitmq.mialkin.svc.cluster.local' \
--from-literal=username='rabbitmq-admin' \
--from-literal=password='rabbitmq-password'
```

## Running in GKE

```bash
cd deploy
kubectl apply -f deployment.yaml
```

## Set bot webhook

Run this in web browser:

```text
https://api.telegram.org/bot<YOUR_TOKEN>/setWebhook?url=WEBHOOK_URL
```

## Local testing with ngrok

Use [ngrok](https://ngrok.com) tool for testing webhooks locally:

```bash
./ngrok http 5110
```
