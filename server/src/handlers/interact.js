const { WebcastEvent } = require("tiktok-live-connector");

const tiktokConnection = require("../connections/tiktok");

const sendToUnity = require("./send_to_unity");

const constants = require("../untils/constants");
const giftConfig = require("../untils/gift_config");
// console.log(giftConfig);

tiktokConnection.on(WebcastEvent.GIFT, (data) => {
  // console.log(data);
  console.log(`${data.giftDetails.giftName} x${data.repeatCount}, ${data.test}`);

  sendToUnity({ ...giftInfo, count: data.test ? 1 : data.repeatCount });
});
