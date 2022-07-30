<template>
    <div id="particles-js" ref='particlesRef'>
        <canvas ref="canvasRef"></canvas>
    </div>
</template>

<script setup>
import { reactive, ref, onMounted } from "vue";
const canvasRef = ref()
onMounted(() => {
    // 可调参数
    var BACKGROUND_COLOR = "#000000"; // 背景颜色
    var POINT_NUM = 200; // 星星数目
    var POINT_COLOR = "#ffff33"; // 点的颜色
    var LINE_LENGTH = 10000; // 点之间连线长度(的平方)
    // 设置画布参数
    var cvs = canvasRef.value;
    cvs.width = window.innerWidth;
    cvs.height = window.innerHeight;
    cvs.style.cssText = "\
    position:fixed;\
    top:0px;\
    left:0px;\
    z-index:-1;\
    opacity:1.0;\
    ";

    var ctx = cvs.getContext("2d");
    var startTime = new Date().getTime();
    //随机数函数
    function randomInt(min, max) {
        return Math.floor((max - min + 1) * Math.random() + min);
    }

    function randomFloat(min, max) {
        return (max - min) * Math.random() + min;
    }
    //构造点类
    function Point() {
        this.x = randomFloat(0, cvs.width);
        this.y = randomFloat(0, cvs.height);
        var speed = randomFloat(0.3, 1.4);
        var angle = randomFloat(0, 2 * Math.PI);
        this.dx = Math.sin(angle) * speed;
        this.dy = Math.cos(angle) * speed;
        this.r = 1.2;
        this.color = POINT_COLOR;
    }
    Point.prototype.move = function () {
        this.x += this.dx;
        if (this.x < 0) {
            this.x = 0;
            this.dx = -this.dx;
        } else if (this.x > cvs.width) {
            this.x = cvs.width;
            this.dx = -this.dx;
        }
        this.y += this.dy;
        if (this.y < 0) {
            this.y = 0;
            this.dy = -this.dy;
        } else if (this.y > cvs.height) {
            this.y = cvs.height;
            this.dy = -this.dy;
        }
    }
    Point.prototype.draw = function () {
        ctx.fillStyle = this.color;
        ctx.beginPath();
        ctx.arc(this.x, this.y, this.r, 0, Math.PI * 2);
        ctx.closePath();
        ctx.fill();
    }
    var points = [];

    function initPoints(num) {
        for (var i = 0; i < num; ++i) {
            points.push(new Point());
        }
    }
    var p0 = new Point(); //鼠标
    p0.dx = p0.dy = 0;
    var degree = 2.5;
    document.onmousemove = function (ev) {
        p0.x = ev.clientX;
        p0.y = ev.clientY;
    }
    document.onmousedown = function (ev) {
        degree = 5.0;
        p0.x = ev.clientX;
        p0.y = ev.clientY;
    }
    document.onmouseup = function (ev) {
        degree = 2.5;
        p0.x = ev.clientX;
        p0.y = ev.clientY;
    }
    window.onmouseout = function () {
        p0.x = null;
        p0.y = null;
    }

    function drawLine(p1, p2, deg) {
        var dx = p1.x - p2.x;
        var dy = p1.y - p2.y;
        var dis2 = dx * dx + dy * dy;
        if (dis2 < 2 * LINE_LENGTH) {
            if (dis2 > LINE_LENGTH) {
                if (p1 === p0) {
                    p2.x += dx * 0.03;
                    p2.y += dy * 0.03;
                } else return;
            }
            var t = (1.05 - dis2 / LINE_LENGTH) * 0.2 * deg;
            ctx.strokeStyle = "rgba(255,255,255," + t + ")";
            ctx.beginPath();
            ctx.lineWidth = 1.5;
            ctx.moveTo(p1.x, p1.y);
            ctx.lineTo(p2.x, p2.y);
            ctx.closePath();
            ctx.stroke();
        }
        return;
    }
    //绘制每一帧
    function drawFrame() {
        cvs.width = window.innerWidth;
        cvs.height = window.innerHeight;
        ctx.fillStyle = BACKGROUND_COLOR;
        ctx.fillRect(0, 0, cvs.width, cvs.height);
        var arr = (p0.x == null ? points : [p0].concat(points));
        for (var i = 0; i < arr.length; ++i) {
            for (var j = i + 1; j < arr.length; ++j) {
                drawLine(arr[i], arr[j], 1.0);
            }
            arr[i].draw();
            arr[i].move();
        }
        window.requestAnimationFrame(drawFrame);
    }
    initPoints(POINT_NUM);
    drawFrame();
    // particles粒子动画
    particlesJS('particles-js', {
        "particles": {
            "number": {
                "value": 100,
                "density": {
                    "enable": false,
                    "value_area": 40
                }
            },
            "color": {
                "value": "#ffffff"
            },
            "shape": {
                "type": ["circle", "edge", "polygon", "star", "triangle"],
                "stroke": {
                    "width": 0,
                    "color": "#ffffff"
                },
                "polygon": {
                    "nb_sides": 6
                }
            },
            "opacity": {
                "value": 1,
                "random": true,
                "anim": {
                    "enable": true,
                    "speed": 5,
                    "opacity_min": 0.1,
                    "sync": false
                }
            },
            "size": {
                "value": 5,
                "random": true,
                "anim": {
                    "enable": true,
                    "speed": 4,
                    "size_min": 0.1,
                    "sync": false
                }
            },
            "line_linked": {
                "enable": true,
                "distance": 150,
                "color": "#ffffff",
                "opacity": 0.4,
                "width": 2
            },
            "move": {
                "enable": true,
                "speed": 3,
                "direction": "none",
                "random": false,
                "straight": false,
                "out_mode": "bounce",
                "attract": {
                    "enable": false,
                    "rotateX": 150,
                    "rotateY": 150
                }
            }
        },
        "interactivity": {
            "detect_on": "canvas",
            "events": {
                "onhover": {
                    "enable": true,
                    "mode": ["grab"]
                },
                "onclick": {
                    "enable": true,
                    "mode": "push"
                },
                "resize": false,
                "modes": {
                    "grab": {
                        "distance": 400,
                        "line_linked": {
                            "opacity": 1
                        }
                    },
                    "bubble": {
                        "distance": 400,
                        "size": 40,
                        "duration": 2,
                        "opacity": 8,
                        "speed": 3
                    },
                    "repulse": {
                        "distance": 200
                    },
                    "push": {
                        "particles_nb": 4
                    },
                    "remove": {
                        "particles_nb": 2
                    }
                }
            },
        },
        "retina_detect": true,
        "config_demo": {
            "hide_card": false,
            "background_color": "#b61924",
            "background_image": "",
            "background_position": "50% 50%",
            "background_repeat": "no-repeat",
            "background_size": "cover"
        }
    });
})


</script>

<style lang="less" scoped>
#particles-js {
    width: 100%;
    height: 100%;
    /*     background: #000; */
}
</style>
